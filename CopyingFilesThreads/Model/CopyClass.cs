using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace CopyingFilesThreads.Model
{

    public delegate void ProgressChangeDelegate(double Persentage, ref bool Cancel, Grid panel);

    public delegate void Completedelegate(Grid panel);



    class CopyClass
    {
        public Grid _panel;
        public bool cancelFlag = false;
        public ManualResetEvent pauseFlag = new ManualResetEvent(true);

        public CopyClass(string Source, string Dest, ProgressChangeDelegate onProgressChanged, Completedelegate onComplete, Grid panel)
        {

            this.SourceFilePath = Source;

            this.DestFilePath = Dest;

            _panel = panel;

            OnProgressChanged += onProgressChanged;

            OnComplete += onComplete;
        }



        public void Copy()
        {

            byte[] buffer = new byte[1024 * 1024]; // 1MB buffer

            bool isCopy = true;
            while (isCopy)
            {
                try
                {
                    using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
                    {

                        long fileLength = source.Length;

                        using (FileStream dest = new FileStream(DestFilePath, FileMode.CreateNew, FileAccess.Write))
                        {

                            long totalBytes = 0;

                            int currentBlockSize = 0;



                            while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                totalBytes += currentBlockSize;

                                double persentage = (double) totalBytes * 100.0 / fileLength;



                                dest.Write(buffer, 0, currentBlockSize);



                                OnProgressChanged(persentage, ref cancelFlag, _panel);

                                if (CancelFlag == true)
                                {
                                    File.Delete(DestFilePath);
                                    // Delete dest file here
                                    isCopy = false;
                                    break;
                                }

                                CancelFlag = false;
                                pauseFlag.WaitOne(Timeout.Infinite);

                            }

                        }

                    }
                    isCopy = false;
                }
                catch (IOException e)
                {
                    if (!cancelFlag)
                    {
                        var res = MessageBox.Show(e.Message + " Replace?", "Replace?", MessageBoxButton.YesNo,
                            MessageBoxImage.Question);
                        if (res == MessageBoxResult.Yes)
                        {
                            isCopy = true;
                            File.Delete(DestFilePath);
                        }
                        else
                        {
                            isCopy = false;
                        }
                    }
                    else
                    {
                        var res = MessageBox.Show(" Copying was canceled!", "Cancel", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        isCopy = false;
                        File.Delete(DestFilePath);
                    }
                }
                catch (Exception e)
                {
                    var res = MessageBox.Show(e.Message, "Oooops", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            



            OnComplete(_panel);

        }



        public string SourceFilePath { get; set; }

        public string DestFilePath { get; set; }

        public bool CancelFlag
        {
            get { return cancelFlag; }
            set { cancelFlag = value; }
        }

        public ManualResetEvent PauseFlag
        {
            get { return pauseFlag; }
            set { pauseFlag = value; }
        }

        public event ProgressChangeDelegate OnProgressChanged;

        public event Completedelegate OnComplete;

    }
}

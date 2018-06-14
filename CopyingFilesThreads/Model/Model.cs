using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;

namespace CopyingFilesThreads.Model
{
    public class Model
    {
        private CopyPathes _pathes;

        public Model()
        {
            _pathes = new CopyPathes();
        }
        
        public CopyPathes Pathes
        {
            get { return _pathes; }
            set { _pathes = value; }
        }

        public void DoCopy(ProgressChangeDelegate ProgressChanged, Completedelegate ModelOnOnComplete, Grid panel)
        {
            var newCopy = new CopyClass(_pathes.FromPath, _pathes.ToPath, ProgressChanged, ModelOnOnComplete, panel);
            panel.Tag = newCopy;
            Thread newThread = new Thread(new ThreadStart(newCopy.Copy));
            newThread.IsBackground = true;
            newThread.Start();
        }
    }
}
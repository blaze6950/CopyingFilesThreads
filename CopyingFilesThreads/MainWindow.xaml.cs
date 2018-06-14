using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CopyingFilesThreads.Presenter;
using CopyingFilesThreads.View;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace CopyingFilesThreads
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IViewMainWindow
    {
        private IPresenterMainWindow _presenter;

        public MainWindow()
        {
            InitializeComponent();
            _presenter = new PresenterMainWindow(this);
        }

        private void CopyButton_OnClick(object sender, RoutedEventArgs e)
        {
            _presenter.CopyButtonClick();
        }

        private void ChooseFileFromButton_OnClick(object sender, RoutedEventArgs e)
        {
            FromTextBox.Text = OpenFile();
            _presenter.ChooseFileFromButtonClick(ToTextBox.Text);
        }

        private void ChooseFileToButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                ToTextBox.Text = dialog.SelectedPath;
                _presenter.ChooseFileToButtonClick(ToTextBox.Text);
            }
            
        }

        private string OpenFile()
        {
            var openFile = new OpenFileDialog();
            openFile.Multiselect = false;
            openFile.ShowDialog();
            return openFile.FileName;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ToTextBox.Text.Length > 0 && FromTextBox.Text.Length > 0)
            {
                CopyButton.IsEnabled = true;
            }
            else
            {
                CopyButton.IsEnabled = false;
            }
        }
        
        public MainWindow ViewMainWindow { get => this; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CopyingFilesThreads.Presenter;
using CopyingFilesThreads.View;

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

        private void ChooseFileButton_OnClick(object sender, RoutedEventArgs e)
        {
            _presenter.ChooseFileButtonClick();
        }

        private void PauseButton_OnClick(object sender, RoutedEventArgs e)
        {
            _presenter.PauseButtonClick();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            _presenter.CancelButtonClick();
        }
    }
}

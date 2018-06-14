using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using CopyingFilesThreads.Model;
using CopyingFilesThreads.View;

namespace CopyingFilesThreads.Presenter
{
    public class PresenterMainWindow : IPresenterMainWindow
    {
        private IViewMainWindow _view;
        private Model.Model _model;

        public PresenterMainWindow(IViewMainWindow view)
        {
            _view = view;
            _model = new Model.Model();
        }

        public void CopyButtonClick()
        {
            _model.Pathes.FromPath = _view.ViewMainWindow.FromTextBox.Text;
            _model.Pathes.ToPath = _view.ViewMainWindow.ToTextBox.Text;
            _view.ViewMainWindow.FromTextBox.Text = "";
            _view.ViewMainWindow.ToTextBox.Text = "";
            _view.ViewMainWindow.Height = _view.ViewMainWindow.Height + 60;
            Grid newPanel = new Grid();
            newPanel.ColumnDefinitions.Add(new ColumnDefinition{Width = new GridLength(320)});
            newPanel.ColumnDefinitions.Add(new ColumnDefinition());
            newPanel.ColumnDefinitions.Add(new ColumnDefinition());
            newPanel.RowDefinitions.Add(new RowDefinition{Height = new GridLength(20)});
            newPanel.RowDefinitions.Add(new RowDefinition());
            var nameFile = new TextBlock
            {
                Text = Path.GetFileName(_model.Pathes.FromPath),
                Margin = new Thickness(5, 0, 5, 0)
            };
            Grid.SetRow(nameFile, 0);
            Grid.SetColumn(nameFile, 0);
            newPanel.Children.Add(nameFile);
            var progressBar = new ProgressBar
            {
                Margin = new Thickness(10, 10, 10, 10)
            };
            Grid.SetRow(progressBar, 1);
            newPanel.Children.Add(progressBar);
            var pauseB = new Button
            {
                Content = "Pause",
                Margin = new Thickness(5),
                Tag = newPanel
            };
            pauseB.Click += PauseCancelBOnClick;
            Grid.SetRow(pauseB, 1);
            Grid.SetColumn(pauseB, 1);
            newPanel.Children.Add(pauseB);
            var cancelB = new Button
            {
                Content = "Cancel",
                Margin = new Thickness(5),
                Tag = newPanel
            };
            cancelB.Click += PauseCancelBOnClick;
            Grid.SetRow(cancelB, 1);
            Grid.SetColumn(cancelB, 2);
            newPanel.Children.Add(cancelB);
            DockPanel.SetDock(newPanel, Dock.Top);
            newPanel.Height = 60;
            _view.ViewMainWindow.MainPanel.Children.Add(newPanel);
            _model.DoCopy(ProgressChanged, ModelOnOnComplete, newPanel);
        }

        private void PauseCancelBOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            (sender as Button).IsEnabled = false;
            if ((sender as Button).Content.ToString().Equals("Cancel"))
            {
                (((sender as Button).Tag as Grid).Tag as CopyClass).CancelFlag = true;
            }
            else if ((sender as Button).Content.ToString().Equals("Pause"))
            {
                (((sender as Button).Tag as Grid).Tag as CopyClass).PauseFlag.Reset();
            }
            else
            {
                (((sender as Button).Tag as Grid).Tag as CopyClass).PauseFlag.Set();
            }
        }

        private void ModelOnOnComplete(Grid panel)
        {
            _view.ViewMainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    _view.ViewMainWindow.Height = _view.ViewMainWindow.Height - 60;
                    _view.ViewMainWindow.MainPanel.Children.Remove(panel);
                    //_view.ViewMainWindow.CopyButton.IsEnabled = true;
                }
            );
        }

        public void ChooseFileFromButtonClick(string path)
        {
            _model.Pathes.FromPath = path;
        }

        public void ChooseFileToButtonClick(string path)
        {
            _model.Pathes.ToPath = path;
        }

        public void ProgressChanged(double persentage, ref bool cancelFlag, Grid panel)
        {
            _view.ViewMainWindow.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
                (ThreadStart)delegate ()
                {
                    foreach (var el in panel.Children)
                    {
                        if (el is ProgressBar)
                        {
                            (el as ProgressBar).Value = persentage;
                        }
                        if (el is Button && (el as Button).Content.ToString().Equals("Resume") && (el as Button).IsEnabled == false)
                        {
                            (el as Button).Content = "Pause";
                            (el as Button).IsEnabled = true;
                        }
                        else if (el is Button && (el as Button).Content.ToString().Equals("Pause") && (el as Button).IsEnabled == false)
                        {
                            (el as Button).Content = "Resume";
                            (el as Button).IsEnabled = true;
                        }
                    }
                }
            );
        }
    }
}
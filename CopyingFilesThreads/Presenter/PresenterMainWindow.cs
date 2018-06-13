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
        }

        public void CopyButtonClick()
        {
            throw new System.NotImplementedException();
        }

        public void ChooseFileButtonClick()
        {
            throw new System.NotImplementedException();
        }

        public void PauseButtonClick()
        {
            throw new System.NotImplementedException();
        }

        public void CancelButtonClick()
        {
            throw new System.NotImplementedException();
        }
    }
}
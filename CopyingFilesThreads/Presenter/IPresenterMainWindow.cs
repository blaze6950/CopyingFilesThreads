using System;

namespace CopyingFilesThreads.Presenter
{
    public interface IPresenterMainWindow
    {
        void CopyButtonClick();

        void ChooseFileFromButtonClick(String path);

        void ChooseFileToButtonClick(String path); 
    }
}
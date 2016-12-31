using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.Storage;

namespace SteveHemond.MusicSheetViewer.ViewModels
{
    public class PdfDisplayPageViewModel : ViewModelBase
    {
        public bool IsFullScreen { get { return ApplicationView.GetForCurrentView().IsFullScreen; } }

        private StorageFile pdfFile;
        public StorageFile PdfFile
        {
            get { return pdfFile; }
            set { SetProperty(ref pdfFile, value); }
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".pdf");
            PdfFile = await picker.PickSingleFileAsync();
        }
    }
}
using System;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.Storage;
using Caliburn.Micro;

namespace SteveHemond.MusicSheetViewer.ViewModels
{
    public class PresentationViewModel : Screen
    {
        public bool IsFullScreen { get { return ApplicationView.GetForCurrentView().IsFullScreen; } }

        public StorageFile PdfFile { get; set; }

        public PresentationViewModel()
        {
            GetFile();
        }

        public async void GetFile()
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".pdf");
            PdfFile = await picker.PickSingleFileAsync();
        }
    }
}
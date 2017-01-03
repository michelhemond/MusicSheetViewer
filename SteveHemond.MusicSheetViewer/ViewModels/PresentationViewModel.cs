﻿using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using System;
using System.Collections.Generic;
using Windows.Storage.Pickers;
using Windows.UI.ViewManagement;
using Windows.Storage;
using System.Threading.Tasks;

namespace SteveHemond.MusicSheetViewer.ViewModels
{
    public class PresentationViewModel : ViewModelBase
    {
        public bool IsFullScreen { get { return ApplicationView.GetForCurrentView().IsFullScreen; } }

        private StorageFile pdfFile;
        public StorageFile PdfFile
        {
            get { return pdfFile; }
            set { SetProperty(ref pdfFile, value); }
        }
        public PresentationViewModel()
        {
            GetFile();
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            //ApplicationView.GetForCurrentView().TryEnterFullScreenMode();
            
        }

        public async void GetFile()
        {
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".pdf");
            PdfFile = await picker.PickSingleFileAsync();
        }
    }
}
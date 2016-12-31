using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace SteveHemond.Controls.PdfViewer
{
    public sealed partial class PdfViewerControl : UserControl
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source",
            typeof(StorageFile),
            typeof(PdfViewerControl),
            new PropertyMetadata(null, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pdfViewer = (PdfViewerControl)d;
            pdfViewer.Source = (StorageFile)e.NewValue;
        }

        public StorageFile Source
        {
            get { return (StorageFile)GetValue(SourceProperty); }
            set
            {
                var pdfFile = value;
                SetValue(SourceProperty, pdfFile);
                FillPages(pdfFile);
                Refresh();
            }
        }

        private int currentPageIndex = 0;
        
        private ObservableCollection<PageTuple> SinglePages = new ObservableCollection<PageTuple>();
        private ObservableCollection<PageTuple> DoublePages = new ObservableCollection<PageTuple>();

        public PdfViewerControl()
        {
            InitializeComponent();
        }

        private void Refresh()
        {
            var o = ApplicationView.GetForCurrentView().Orientation;

            if (o.Equals(ApplicationViewOrientation.Landscape))
            {
                myPivot.ItemsSource = DoublePages;
                myPivot.ItemTemplate = (DataTemplate)Resources["Two"];
            }
            else
            {
                myPivot.ItemsSource = SinglePages;
                myPivot.ItemTemplate = (DataTemplate)Resources["One"];
            }
        }

        public async void FillPages(StorageFile storageFile)
        {
            var pdfDocument = await PdfDocument.LoadFromFileAsync(storageFile);

            for (uint i = 0; i < pdfDocument.PageCount; i += 2)
            {
                var pageTupleForDoublePage = new PageTuple();
                var pageTupleForSinglePage1 = new PageTuple();
                var pageTupleForSinglePage2 = new PageTuple();

                if (i == pdfDocument.PageCount - 1)
                {
                    var page = await GetImageFromPageAsync(pdfDocument, i);
                    pageTupleForDoublePage.Page1 = page;
                    pageTupleForSinglePage1.Page1 = page;
                }
                else if (i + 1 < pdfDocument.PageCount)
                {
                    var page1 = await GetImageFromPageAsync(pdfDocument, i);
                    var page2 = await GetImageFromPageAsync(pdfDocument, i + 1);

                    pageTupleForDoublePage.Page1 = page1;
                    pageTupleForDoublePage.Page2 = page2;

                    pageTupleForSinglePage1.Page1 = page1;
                    pageTupleForSinglePage2.Page1 = page2;
                }

                SinglePages.Add(pageTupleForSinglePage1);
                SinglePages.Add(pageTupleForSinglePage2);
                DoublePages.Add(pageTupleForDoublePage);
            }
        }

        public async Task<BitmapImage> GetImageFromPageAsync(PdfDocument pdfDocument, uint index)
        {
            using (var page = pdfDocument.GetPage(index))
            {
                var stream = new InMemoryRandomAccessStream();
                await page.RenderToStreamAsync(stream);
                var bitmapImage = new BitmapImage();
                await bitmapImage.SetSourceAsync(stream);
                return bitmapImage;
            }
        }

        private void myPivot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Refresh();
        }
    }
}
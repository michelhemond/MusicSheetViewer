using Prism.Windows.Mvvm;
using SteveHemond.MusicSheetViewer.Data;
using System.Collections.ObjectModel;

namespace SteveHemond.MusicSheetViewer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private MenuItem selectedItem;
        public MenuItem SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }
        public MainViewModel()
        {
            MenuItems = GetMenuItems();
        }

        private ObservableCollection<MenuItem> GetMenuItems()
        {
            return new ObservableCollection<MenuItem>
            {
                new MenuItem { Icon = "\uE10F", Name = "Item 1", Target = null },
                new MenuItem { Icon = "\uE77B", Name = "Item 2", Target = null }
            };
        }

        public ObservableCollection<MenuItem> MenuItems { get; set; }
    }
}
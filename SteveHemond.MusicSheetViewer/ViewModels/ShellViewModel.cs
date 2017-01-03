using Prism.Windows.Mvvm;
using SteveHemond.MusicSheetViewer.Data;
using SteveHemond.MusicSheetViewer.Views;
using System.Collections.ObjectModel;

namespace SteveHemond.MusicSheetViewer.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private MenuItem selectedItem;
        public MenuItem SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }
        public ShellViewModel()
        {
            MenuItems = GetMenuItems();
        }

        private ObservableCollection<MenuItem> GetMenuItems()
        {
            return new ObservableCollection<MenuItem>
            {
                new MenuItem { Icon = "\uE10F", Name = "Accueil", Target = typeof(Main) },
                new MenuItem { Icon = "\uE77B", Name = "Présentation", Target = typeof(Presentation) }
            };
        }

        public ObservableCollection<MenuItem> MenuItems { get; set; }
    }
}
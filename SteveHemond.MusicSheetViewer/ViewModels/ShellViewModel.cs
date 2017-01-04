using Caliburn.Micro;
using SteveHemond.MusicSheetViewer.Data;
using SteveHemond.MusicSheetViewer.Views;
using System.Collections.ObjectModel;

namespace SteveHemond.MusicSheetViewer.ViewModels
{
    public class ShellViewModel : Screen
    {
        public MenuItem SelectedItem { get; set; }

        public ShellViewModel()
        {
            MenuItems = GetMenuItems();
        }

        private ObservableCollection<MenuItem> GetMenuItems()
        {
            return new ObservableCollection<MenuItem>
            {
                new MenuItem { Icon = "\uE10F", Name = "Accueil", Target = typeof(WelcomeView) },
                new MenuItem { Icon = "\uE77B", Name = "Présentation", Target = typeof(PresentationView) }
            };
        }

        public ObservableCollection<MenuItem> MenuItems { get; set; }
    }
}
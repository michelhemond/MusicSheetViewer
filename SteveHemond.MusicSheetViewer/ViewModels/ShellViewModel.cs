using Caliburn.Micro;
using SteveHemond.MusicSheetViewer.Data;
using SteveHemond.MusicSheetViewer.Messages;
using SteveHemond.MusicSheetViewer.Views;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace SteveHemond.MusicSheetViewer.ViewModels
{
    public class ShellViewModel : Screen, IHandle<ResumeStateMessage>, IHandle<SuspendStateMessage>
    {
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

        private readonly WinRTContainer container;

        private readonly IEventAggregator eventAggregator;

        private INavigationService navigationService;

        private bool resume;

        public ShellViewModel(WinRTContainer container, IEventAggregator eventAggregator)
        {
            this.container = container;
            this.eventAggregator = eventAggregator;
            MenuItems = GetMenuItems();
        }

        protected override void OnActivate()
        {
            eventAggregator.Subscribe(this);
        }

        protected override void OnDeactivate(bool close)
        {
            eventAggregator.Unsubscribe(this);
        }

        public void SetupNavigationService(Frame frame)
        {
            navigationService = container.RegisterNavigationService(frame);
            if (resume) navigationService.ResumeState();
        }

        public void NavigateTo(object source)
        {
            var stackPanel = source as StackPanel;
            var menuItem = stackPanel.DataContext as MenuItem;
            var target = menuItem.Target;
            navigationService.Navigate(target);
        }

        public void ShowWelcome()
        {
            navigationService.For<WelcomeViewModel>().Navigate();
        }

        public void Handle(SuspendStateMessage message)
        {
            navigationService.SuspendState();
        }

        public void Handle(ResumeStateMessage message)
        {
            resume = true;
        }
    }
}
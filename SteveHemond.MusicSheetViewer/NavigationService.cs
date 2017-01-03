using Microsoft.Toolkit.Uwp.UI.Controls;
using SteveHemond.MusicSheetViewer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace SteveHemond.MusicSheetViewer
{
    public static class ApplicationService
    {
        private static Frame contentFrame;

        private static HamburgerMenu menu;

        public static void RegisterHamburgerMenu(HamburgerMenu hamburgerMenu1)
        {
            menu = hamburgerMenu1;
        }

        public static void RegisterContentFrame(Frame frame)
        {
            contentFrame = frame;
        }

        public static void NavigateTo(Type target)
        {
            contentFrame.Navigate(target);
        }

        public static void HideHamburgerMenu() => menu.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

        public static void ShowHamburgerMenu() => menu.Visibility = Windows.UI.Xaml.Visibility.Visible;
    }
}
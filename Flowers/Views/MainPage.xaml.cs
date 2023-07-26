using System.Windows;
using MSB.UI.Controls;
using Flowers.Controls;
using Flowers.Helpers;
using System;
using System.Windows.Controls;
using System.Linq;

namespace Flowers.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    sealed partial class MainPage : AeroPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var nav = (this.Content as NavigationView);

            NavigationManager.RegisterFrame("MainFrame", nav.Content as AeroFrame);

            (nav.PaneCustomContent as StackPanel).Children.OfType<AppBarButton>()
                .ToList().ForEach(btn => btn.Click += NavigationButton_Click);

            nav.SelectedItem = nav.MenuItems[0];
        }

        private void OnSelectionChanged(object sender, RoutedEventArgs args)
        {
            var nav = (sender as NavigationView);

            if (nav.SelectedItem is null)
                return;

            var item = nav.SelectedItem as NavigationViewItem;

            NavigationManager.Navigate("MainFrame", item.SourcePageType, item.Tag);
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as AppBarButton;

            NavigationManager.Navigate("FlyoutFrame", btn.Tag as Type);

            App.GetCurrentView().Flyout.IsOpen = true;
        }
    }
}

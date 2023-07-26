using System.Windows;
using Flowers.Controls;
using Flowers.Helpers;
using Flowers.SubViews;

namespace Flowers.Views
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    sealed partial class HomePage : AeroPage
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        private void ShowCreditsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.Navigate<CreditsPage>("FlyoutFrame");

            App.GetCurrentView().Flyout.IsOpen = true;
        }
    }
}

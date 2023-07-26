using System.Windows;
using Flowers.Controls;
using Flowers.Helpers;
using Flowers.Views;
using MSB.UI;

namespace Flowers
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.
        /// </summary>
        /// <param name="e">Details about the startup request and process.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            var flyoutFrame = new AeroFrame();

            NavigationManager.RegisterFrame("FlyoutFrame", flyoutFrame);

            this.MainWindow = new AeroWindow
            {
                Title = "Flowers",
                ExtendViewIntoTitleBar = true,
                WindowState = WindowState.Maximized,
                ShowActivated = true,
                MinHeight = 550,
                MinWidth = 600,
                Content = new MainPage(),
                Flyout = new()
                {
                    Content = flyoutFrame
                }
            };

            this.MainWindow.Show();
        }

        /// <summary>
        /// Gets the current view.
        /// </summary>
        /// <returns>The current view, if the current <see cref="Window"/> is an <see cref="AeroWindow"/>; otherwise, **<see langword="null"/>**.</returns>
        #nullable enable
        public static AeroWindow? GetCurrentView()
        {
            return (AeroWindow)Current.MainWindow;
        }
    }
}

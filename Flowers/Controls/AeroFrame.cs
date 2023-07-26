using System.Windows.Controls;
using System.Windows.Navigation;

namespace Flowers.Controls
{
    sealed class AeroFrame : Frame
    {
        public AeroFrame()
        {
            this.Navigated += OnNavigated;
            this.Navigating += OnNavigating;
        }

        #region Methods

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            last_page?.OnNavigatedFrom(e);

            if (Content is AeroPage page)
            {
                page.OnNavigatedTo(e);
                last_page = page;
            }
        }

        private void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            last_page?.OnNavigatingFrom(e);
        }

        #endregion

        AeroPage last_page = null;
    }
}

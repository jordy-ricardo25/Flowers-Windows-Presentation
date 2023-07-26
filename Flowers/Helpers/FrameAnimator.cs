using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Controls;
using System.Windows;

namespace Flowers.Helpers
{
    static class FrameHelper
    {
        #region Properties

        public static Storyboard GetNavigationNextStoryboard(DependencyObject control)
        {
            return (Storyboard)control.GetValue(NavigationNextStoryboardProperty);
        }

        public static void SetNavigationNextStoryboard(DependencyObject control, Storyboard st)
        {
            control.SetValue(NavigationNextStoryboardProperty, st);
        }

        public static Storyboard GetNavigationBackStoryboard(DependencyObject control)
        {
            return (Storyboard)control.GetValue(NavigationBackStoryboardProperty);
        }

        public static void SetNavigationBackStoryboard(DependencyObject control, Storyboard st)
        {
            control.SetValue(NavigationBackStoryboardProperty, st);
        }

        #endregion

        #region Depdency properties

        /// <summary>
        /// Identifies the NavigationNextStoryboard dependency property.
        /// </summary>
        public static readonly DependencyProperty NavigationNextStoryboardProperty =
                DependencyProperty.RegisterAttached("NavigationNextStoryboard", typeof(Storyboard), typeof(FrameHelper), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, NavigationNextStoryboardChanged_Callback));

        /// <summary>
        /// Identifies the NavigationBackStoryboard dependency property.
        /// </summary>
        public static readonly DependencyProperty NavigationBackStoryboardProperty =
                DependencyProperty.RegisterAttached("NavigationBackStoryboard", typeof(Storyboard), typeof(FrameHelper), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsMeasure, NavigationBackStoryboardChanged_Callback));

        #endregion

        #region Callbacks

        private static void NavigationNextStoryboardChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Frame f)
                return;

            var st = GetNavigationNextStoryboard(d);

            if (st is null)
                return;

            f.Navigating += (sender, e) =>
            {
                if (e.NavigationMode is not NavigationMode.Back)
                    st.Begin(f);
            };
        }

        private static void NavigationBackStoryboardChanged_Callback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not Frame f)
                return;

            var st = GetNavigationBackStoryboard(d);

            if (st is null)
                return;

            f.Navigating += (sender, e) =>
            {
                if (e.NavigationMode is NavigationMode.Back)
                    st.Begin(f);
            };
        }

        #endregion
    }
}

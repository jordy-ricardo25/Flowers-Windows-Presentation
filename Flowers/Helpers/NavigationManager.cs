using System.Collections.Generic;
using System.Windows.Controls;
using System;

namespace Flowers.Helpers
{
    /// <summary>
    /// Provides methods for managing App navigation between frames.
    /// </summary>
    static class NavigationManager
    {
        /// <summary>
        /// Register a frame with a key to be used in navigation.
        /// </summary>
        /// <param name="key">The key to use for the frame.</param>
        /// <param name="frame">The frame to register.</param>
        /// <returns>**<see langword="true"/>** if the frame was registered, **<see langword="false"/>** if the key already exists.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool RegisterFrame(string key, Frame frame)
        {
            if (frame is null)
                throw new ArgumentNullException(nameof(frame));

            if (frames.ContainsKey(key))
                return false;

            frames.Add(key, frame);

            return true;
        }

        /// <summary>
        /// Unregister a frame with a key.
        /// </summary>
        /// <param name="key">The key of the frame to unregister.</param>
        /// <returns>**<see langword="true"/>** if the frame was unregistered, **<see langword="false"/>** if the key does not exist.</returns>
        public static bool UnregisterFrame(string key)
        {
            return frames.Remove(key);
        }

        /// <summary>
        /// Navigate to an object using the frame with the specified key.
        /// </summary>
        /// <param name="key">The key of the frame to navigate.</param>
        /// <param name="target">The object to navigate to.</param>
        /// <param name="parameter">An optional parameter to pass to the target.</param>
        /// <returns>**<see langword="true"/>** if the navigation was successful, otherwise **<see langword="false"/>**.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception> 
        public static bool Navigate(string key, Type target, object parameter = null)
        {
            if (target is null)
                throw new ArgumentNullException(nameof(target));

            if (!frames.ContainsKey(key))
                return false;

            if (!typeof(Page).IsAssignableFrom(target))
                throw new ArgumentException("The target must be a Page.", nameof(target));

            return frames[key].Navigate(Activator.CreateInstance(target), parameter);
        }

        /// <summary>
        /// Navigate to a page using the frame with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the page to navigate to.</typeparam>
        /// <param name="key">The key of the frame to navigate.</param>
        /// <param name="parameter">An optional parameter to pass to the target.</param>
        /// <returns>**<see langword="true"/>** if the navigation was successful, otherwise **<see langword="false"/>**.</returns>
        public static bool Navigate<T>(string key, object parameter = null) where T : Page
        {
            if (!frames.ContainsKey(key))
                return false;

            return frames[key].Navigate(Activator.CreateInstance<T>(), parameter);
        }

        static readonly Dictionary<string, Frame> frames = new();
    }
}

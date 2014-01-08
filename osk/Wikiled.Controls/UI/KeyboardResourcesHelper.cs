using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Wikiled.Controls.UI
{
    /// <summary>
    /// Keyboard resource helper
    /// </summary>
    public static class KeyboardResourcesHelper
    {
        public static object FindResource(string name)
        {
            if (string.IsNullOrEmpty(name) ||
                Application.Current == null ||
                Application.Current.Resources == null)
            {
                return null; 
            }

            if (Application.Current.Resources.Contains(name))
            {
                return Application.Current.Resources[name];
            }

            var page = Application.Current.RootVisual as Page;
            if (page == null)
            {
                return null;
            }

            var keybard = page.keyboard;
            if (keybard == null)
            {
                return null;
            }
            return keybard.FindResource(name);
        }

        internal static object FindResource(this FrameworkElement root, string name)
        {
            if (root == null)
            {
                return null;
            }

            if (root.Resources.Contains(name))
            {
                return root.Resources[name];
            }
            try
            {
                return root.FindName(name);
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}

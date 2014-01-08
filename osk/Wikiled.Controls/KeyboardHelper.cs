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
using System.Windows.Threading;
using Wikiled.Controls.Helpers;
using Wikiled.Controls.Keyboard;

namespace Wikiled.Controls
{
    public class KeyboardHelper
    {
        static KeyboardHelper()
        {
            keyboard = new KeyboardControl();
            keyboard.VerticalAlignment = VerticalAlignment.Top;
            keyboard.HorizontalAlignment = HorizontalAlignment.Left;

            tmr = new DispatcherTimer();
            tmr.Tick += tmr_Tick;
            tmr.Interval = new TimeSpan(0, 0, 0, 0, 200);

//#if !SILVERLIGHT
//            window = new Window();            
//            window.SizeToContent = SizeToContent.WidthAndHeight;
//            window.WindowStyle = WindowStyle.None;
//            window.ResizeMode = ResizeMode.NoResize;
//            window.Background = Brushes.Transparent;
//            window.Topmost = true;
//            window.Content = keyboard;
//#else

//#endif

        }

        private static KeyboardControl keyboard;
        private static Window window;
        private static Panel oldPanel;
        private static TextBox oldTb;
        private static bool added = false;

        public static bool GetAttachOSK(DependencyObject obj)
        {
            return (bool)obj.GetValue(AttachOSKProperty);
        }

        public static void SetAttachOSK(DependencyObject obj, bool value)
        {
            obj.SetValue(AttachOSKProperty, value);
        }

        public static readonly DependencyProperty AttachOSKProperty =
            DependencyProperty.RegisterAttached("AttachOSK", typeof(bool), typeof(TextBox), new PropertyMetadata(false, OnAttachOSKChanged));

        private static void OnAttachOSKChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var t = d as TextBox;
            if (t != null)
            {
                t.GotFocus += t_GotFocus;
                t.LostFocus += t_LostFocus;
            }
        }

        private static DispatcherTimer tmr;

        static void tmr_Tick(object sender, EventArgs e)
        {
            if (oldPanel != null)
            {
                oldPanel.Children.Remove(keyboard);
                oldPanel = null;
                added = false;
            }   
        }

        static void t_LostFocus(object sender, RoutedEventArgs e)
        {
            tmr.Start();                     
        }

        static void t_GotFocus(object sender, RoutedEventArgs e)
        {
            tmr.Stop();

            var t = sender as TextBox;
            keyboard.CurrentTextBox = t;
            
            if (oldPanel != null && oldTb != t)
            {
                oldPanel.Children.Remove(keyboard);
                oldPanel = null;
                added = false;
            }

            oldPanel = t.TryFindParent<Grid>();;
            oldTb = t;

            if (oldPanel != null && !added)
            {
                GeneralTransform gt = (t).TransformToVisual(oldPanel);
                Point p = gt.Transform(new Point(0, 0));
                keyboard.Margin = new Thickness(p.X, p.Y + t.ActualHeight, -800, -500);
                oldPanel.Children.Add(keyboard);
                added = true;
            }
        }
        
    }
}

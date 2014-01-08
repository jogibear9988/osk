using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Wikiled.Controls.UI
{
    /// <summary>
    /// Key style converter. Finds defined style by its name
    /// </summary>
    public class KeyStyleConverter : IValueConverter
    {
        #region IValueConverter Members
        public FrameworkElement OwnerControl { get; set; }

        public object Convert(
            object value,
            Type targetType,
            object parameter,
            CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (targetType != typeof (Style))
            {
                throw new InvalidOperationException("The target must be a Style");
            }

            var name = (string) value;

            if (OwnerControl != null && OwnerControl.Resources.Contains(name))
            {
                return OwnerControl.Resources[name];
            }

            return null;
        }

        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}

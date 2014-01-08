using System;
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
    /// Opacity converter
    /// </summary>
    public class OpacityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (targetType != typeof(double))
            {
                throw new InvalidOperationException("The target must be a Visibility");
            }
            if (value.GetType() != typeof(bool))
            {
                throw new InvalidOperationException("Converting value has to be boolean");
            }
            return (bool)value ? 100.0 : 0.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}

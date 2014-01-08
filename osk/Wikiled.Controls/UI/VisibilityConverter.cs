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
using Wikiled.Controls.Keyboard;

namespace Wikiled.Controls.UI
{
    /// <summary>
    /// Indicator visibility converte. 
    /// Main purpose just to change combination key apperance
    /// </summary>
    public class VisibilityConverter : IValueConverter
    {

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            if (targetType != typeof(Visibility))
            {
                throw new InvalidOperationException("The target must be a Visibility");
            }
            var combi = value as CombinationKey;
            if (combi == null) // if type is combination key then it is visible, if not - invisible
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}

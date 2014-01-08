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

namespace Wikiled.Controls.Keyboard
{
    /// <summary>
    /// Cobination key definition
    /// </summary>
    public class CombinationKey : SpecialKey
    {
        /// <summary>
        /// On command changes checked state
        /// </summary>
        /// <param name="parameter"></param>
        protected override void CommandExecuted(string parameter)
        {
            IsChecked = !IsChecked;
        }
    }
}

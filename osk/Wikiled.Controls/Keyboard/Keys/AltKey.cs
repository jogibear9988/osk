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
    /// Alt key
    /// </summary>
    public class AltKey : CombinationKey
    {
        public AltKey()
        {
            Name = "Alt";
            Style = "longButtonFirst";
        }

        /// <summary>
        /// On command changes global alt flag settings
        /// </summary>
        /// <param name="parameter"></param>
        protected override void CommandExecuted(string parameter)
        {
            base.CommandExecuted(parameter);
            status.IsAltOn = this.IsChecked;
        }
    }
}

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
    /// Ctrl key
    /// </summary>
    public class CtrlKey : CombinationKey
    {
        public CtrlKey()
        {
            Name = "Ctrl";
            Style = "longButtonFirst";
        }

        /// <summary>
        /// On command execution changes ctrl global flag
        /// </summary>
        /// <param name="parameter"></param>
        protected override void CommandExecuted(string parameter)
        {
            base.CommandExecuted(parameter);
            status.IsCtrlOn = this.IsChecked;
        }
       
    }
}

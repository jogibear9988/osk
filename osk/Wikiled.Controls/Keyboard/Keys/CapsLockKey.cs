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
    /// Cap lock key
    /// </summary>
    public class CapsLockKey : CombinationKey
    {
        public CapsLockKey()
        {
            Name = "CapsLock";
        }

        /// <summary>
        /// On command execution change global caps flag
        /// </summary>
        /// <param name="parameter"></param>
        protected override void CommandExecuted(string parameter)
        {
            base.CommandExecuted(parameter);
            status.IsCapsOn = this.IsChecked;
        }

        public override string Value
        {
            get
            {
                return "Caps";
            }
            set
            {
            }
        }
    }
}

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
    /// Backspace key
    /// </summary>
    public class BackspaceKey : SpecialKey
    {
        public BackspaceKey()
        {
            Style = "regularButton";
            Name = "BackSpace";
        }
        
        public override string Value
        {
            get
            {
                return "Back";
            }
            set
            {
            }
        }
    }
}

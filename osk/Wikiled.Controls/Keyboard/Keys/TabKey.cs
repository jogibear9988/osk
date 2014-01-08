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
    /// Tab key
    /// </summary>
    public class TabKey : SpecialKey
    {
        public TabKey()
        {
            Style = "longButtonFirst";
            Name = "Tab";
        }

        public override string Value
        {
            get
            {
                return "Tab";
            }
            set
            {
            }
        }
    }
}

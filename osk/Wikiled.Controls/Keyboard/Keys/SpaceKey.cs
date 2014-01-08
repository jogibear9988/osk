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
    /// Space key
    /// </summary>
    public class SpaceKey : SpecialKey
    {
        public SpaceKey()
        {
            Style = "spaceButton";
            Name = "Space";
        }

        public override string Value
        {
            get
            {
                return " ";
            }
            set
            {
            }
        }
    }
}
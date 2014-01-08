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
    /// Delete key
    /// </summary>
    public class DeleteKey : SpecialKey
    {
        public DeleteKey()
        {
            Style = "longButtonFirst";
            Name = "Delete";
        }
        
        public override string Value
        {
            get
            {
                return "Del";
            }
            set
            {
            }
        }
    }
}

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
    public class KeyboardException : Exception
    {
        public KeyboardException(string message)
            : base(message)
        { }

        public KeyboardException()
        { }

        public KeyboardException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

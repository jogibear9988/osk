using System;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Wikiled.Controls.Keyboard;

namespace Wikiled.Controls.Keyboard
{
    public delegate void KeyPressed(object sender, KeyPressedEventArgs args);

    /// <summary>
    /// Key pressed event
    /// </summary>
    public class KeyPressedEventArgs : EventArgs
    {
        public KeyPressedEventArgs(Key key)
        {
            if (key == null)
            {
                throw new ArgumentOutOfRangeException("key");
            }
            Key = key;
            PressedKey = key.Value;
        }

        public KeyPressedEventArgs(string pressedKey)
        {
            PressedKey = pressedKey;
        }

        /// <summary>
        /// Pressed key value. Could be accesed via JavaScript
        /// </summary>
        [ScriptableMember]
        public string PressedKey { get; private set; }

        /// <summary>
        /// Pressed key
        /// </summary>
        public Key Key { get; private set; }
    }

}
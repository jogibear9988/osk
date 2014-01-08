using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using Wikiled.Controls.Helpers;
using Wikiled.Controls.Keyboard;

namespace Wikiled.Controls.Keyboard
{
    /// <summary>
    /// Keyboard Row object
    /// </summary>
    public class KeyboardRow : ObservableCollectionXml<Key>
    {
        protected override Key CreateItem(XmlReader reader)
        {
            return KeyFactory.CreateKey(reader);
        }
    }
}

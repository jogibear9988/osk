using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;
using Wikiled.Controls.Keyboard;

namespace Wikiled.Controls.Keyboard
{
    public static class KeyFactory
    {
        public static Key CreateKey(XmlReader reader)
        {
            Key key = null;
            do
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        key = ConstructKeyItem(reader.Name);
                        var style = reader.GetAttribute("style");
                        if (!string.IsNullOrEmpty(style))
                        {
                            key.Style = style;
                        }

                        var shift = reader.GetAttribute("shift");
                        if (!string.IsNullOrEmpty(shift))
                        {
                            key.ShiftValue = shift;
                        }

                        var alt = reader.GetAttribute("alt");
                        if (!string.IsNullOrEmpty(alt))
                        {
                            key.AltValue = alt;
                        }

                        var dk = reader.GetAttribute("dk");
                        if (!string.IsNullOrEmpty(alt))
                        {
                            key.ControlValue = dk;
                        }
                        break;
                    case XmlNodeType.EndElement:
                        return key;
                    case XmlNodeType.Text:
                        if (key != null &&
                            !string.IsNullOrEmpty(reader.Value))
                        {
                            key.Name = reader.Value;
                            key.RegularValue = reader.Value;
                        }
                        break;
                }
            } while (reader.Read());
            return key;
        }

        private static Key ConstructKeyItem(string name)
        {
            switch (name)
            {
                case "BackspaceKey":
                    return new BackspaceKey();
                case "TabKey":
                    return new TabKey();
                case "CapsLockKey":
                    return new CapsLockKey();
                case "DeleteKey":
                    return new DeleteKey();
                case "EnterKey":
                    return new EnterKey();
                case "ShiftKey":
                    return new ShiftKey();
                case "CtrlKey":
                    return new CtrlKey();
                case "AltKey":
                    return new AltKey();
                case "SpaceKey":
                    return new SpaceKey();
                default:
                    return new Key();
            }
        }
    }
}

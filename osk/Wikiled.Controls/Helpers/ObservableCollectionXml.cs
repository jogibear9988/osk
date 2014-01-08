using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml;

namespace Wikiled.Controls.Helpers
{
    /// <summary>
    /// Constructs observable collection from XmlReader
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableCollectionXml<T> : ObservableCollection<T>, IXmlReadable
        where T : class, new()
    {
        /// <summary>
        /// Read xml file
        /// </summary>
        /// <param name="reader"></param>
        public void ReadXml(XmlReader reader)
        {
            if (reader == null)
            {
                return;
            }
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    var type = Type.GetType("Wikiled.Controls.Keyboard." + reader.Name, false);
                    if (type != null &&
                        typeof(T).IsAssignableFrom(type))
                    {
                        var item = CreateItem(reader);
                        if (item != null)
                        {
                            this.Add(item);
                        }
                    }
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    if (string.Compare(reader.Name, this.GetType().Name) == 0)
                    {
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Create collection item
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        protected virtual T CreateItem(XmlReader reader)
        {
            var item = new T();
            if (item is IXmlReadable)
            {
                ((IXmlReadable) item).ReadXml(reader);
            }
            return item;
        }
    }
}

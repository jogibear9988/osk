using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml;

namespace Wikiled.Controls.Keyboard
{
    /// <summary>
    /// Keyboards layouts manager
    /// </summary>
    public class KeyboardLayouts : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private const string assemblyName = "Wikiled.Controls";
        #region Variables
        private static KeyboardLayouts instance;
        private static object syncRoot = new object();
        private List<KeyboardDefinition> allLayouts = new List<KeyboardDefinition>();
        KeyboardDefinition selectedLayout;
        #endregion

        #region private functions
        /// <summary>
        /// Read layouts data
        /// </summary>
        private void Read()
        {
            // open layouts definition file
            StreamResourceInfo stream =
                Application.GetResourceStream(new Uri(assemblyName + ";component/Layouts/Layouts.xml", UriKind.Relative));
            if (stream == null ||
                stream.Stream == null)
            {
                return;
            }
            string defaultLayout = string.Empty;
            // we use XML reader to reduce size of xap file
            using (stream.Stream)
            {
                using (XmlReader reader = XmlReader.Create(stream.Stream))
                {
                    //reader.MoveToContent();
                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (string.Compare(reader.Name, "Layout") == 0)
                            {
                                var path = reader.GetAttribute("path");
                                var name = reader.GetAttribute("name");
                                if (string.IsNullOrEmpty(path) ||
                                    string.IsNullOrEmpty(name))
                                {
                                    continue;
                                }
                                var keyboard =
                                    KeyboardDefinition.ReadKeyboard(
                                        string.Format(assemblyName + ";component/Layouts/{0}", path));
                                allLayouts.Add(keyboard);
                                if (name == defaultLayout)
                                {
                                    SelectedLayout = keyboard;
                                }
                            }
                            else if (string.Compare(reader.Name, "Layouts") == 0)
                            {
                                defaultLayout = reader.GetAttribute("default");
                            }
                        }
                    }
                }
                allLayouts.Sort((item1, item2) => item1.Name.CompareTo(item2.Name));
                if (SelectedLayout == null &&
                    allLayouts.Count > 0)
                {
                    SelectedLayout = allLayouts[0];
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler Handler = PropertyChanged;
            if (Handler != null)
            {
                Handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Properties
        public static KeyboardLayouts Instance
        {
            get
            {
                if (instance != null)
                {
                    return instance;
                }
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new KeyboardLayouts();
                        instance.Read();
                    }
                    return instance;

                }
            }
        }

        
        public KeyboardDefinition SelectedLayout
        {
            get { return selectedLayout; }
            set
            {
                if (value == selectedLayout)
                {
                    return;
                }
                selectedLayout = value;
                OnPropertyChanged("SelectedLayout");
            }
        }

        /// <summary>
        /// All possible keyboard layouts
        /// </summary>
        public ICollection<KeyboardDefinition> Layouts
        {
            get { return allLayouts;}
        }
        #endregion
    }
}

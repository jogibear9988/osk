using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Serialization;
using Wikiled.Controls.Helpers;
using Wikiled.Controls.Keyboard;


namespace Wikiled.Controls.Keyboard
{
    /// <summary>
    /// Full keyboard definition. This object is exposed for external JavaScript access
    /// </summary>
    public class KeyboardDefinition
    {
        /// <summary>
        /// Key pressed event 
        /// </summary>
        [ScriptableMember]
        public event KeyPressed KeyPressed;

        private KeyboardStatus status;

        #region Constructors

        private KeyboardDefinition()
        {
            Rows = new KeyboardRows();
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Read keyboard defintition from given path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static KeyboardDefinition ReadKeyboard(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentOutOfRangeException(path);
            }
            var keyboard = new KeyboardDefinition();
            keyboard.Initialize(path);
            return keyboard;
        }

        /// <summary>
        /// initialize layout from xml file
        /// </summary>
        /// <param name="path"></param>
        private void Initialize(string path)
        {
            Rows.Clear();
            bool allKeysDefinedInXml = false;
            StreamResourceInfo stream = Application.GetResourceStream(new Uri(path, UriKind.Relative));
            if (stream == null ||
                stream.Stream == null)
            {
                throw new KeyboardException(string.Format("Failed to find layout file: {0}", path));
            }
            CultureInfo keyboardCulture = null;
            // we use XML reader to reduce size of xap file
            using (stream.Stream)
            {
                using (XmlReader reader = XmlReader.Create(stream.Stream))
                {
                    // Parse the file and display each of the nodes.
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (string.Compare(reader.Name, "Rows") == 0)
                            {
                                Rows.ReadXml(reader);
                            }
                            else if (string.Compare(reader.Name, "Keyboard") == 0)
                            {
                                var language = reader.GetAttribute("language");
                                var culture = reader.GetAttribute("culture");
                                bool.TryParse(reader.GetAttribute("all"), out allKeysDefinedInXml); 
                                
                                if (!string.IsNullOrEmpty(language))
                                {
                                    this.Name = language;
                                }
                                if (!string.IsNullOrEmpty(culture))
                                {
                                    keyboardCulture = new CultureInfo(culture);
                                }
                            }
                        }
                    }
                }
                if (!allKeysDefinedInXml) // if all keys are defined in xml we do not need hardcoded insertion
                {
                    AddSpecialKeys();
                }
                status = new KeyboardStatus(keyboardCulture);
                SubscribeToKeyEvents();
            }
        }

        /// <summary>
        /// Subscribe to all key events 
        /// </summary>
        /// <remarks>
        /// There is no unsubscribe event - because primary intension to have collection static - it shouldn't change
        /// </remarks>
        private void SubscribeToKeyEvents()
        {
            foreach (var row in Rows)
            {
                foreach (var key in row)
                {
                    key.RegisterStatusHolder(status);
                    key.Pressed += new EventHandler(key_Pressed);
                }
            }
        }

        /// <summary>
        /// Add special key to current row
        /// </summary>
        private void AddSpecialKeys()
        {
            if (Rows.Count < 4) // incorrect layout
            {
                return;
            }
            Rows[0].Add(new BackspaceKey());
            Rows[1].Insert(0, new TabKey());
            Rows[1].Add(new DeleteKey());
            Rows[2].Insert(0, new CapsLockKey());
            Rows[2].Add(new EnterKey());
            var shift = new ShiftKey();
            Rows[3].Insert(0, shift);
            Rows[3].Add(shift);
            Rows.Add(new KeyboardRow()); // last row
            var ctrl = new CtrlKey();
            var alt = new AltKey();
            Rows[4].Add(ctrl);
            Rows[4].Add(alt);
            Rows[4].Add(new SpaceKey());;
            Rows[4].Add(alt);
            Rows[4].Add(ctrl);
        }

        /// <summary>
        /// If key pressed, bubble event to external listener
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void key_Pressed(object sender, EventArgs e)
        {
            if (KeyPressed != null)
            {
                KeyPressed(this, new KeyPressedEventArgs((Key)sender));
            }
        }
       
        #endregion

        #region properties

        /// <summary>
        /// All rows
        /// </summary>
        public KeyboardRows Rows { get; private set; }

        /// <summary>
        /// Keyboard name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Global keybard status
        /// </summary>
        public KeyboardStatus Status
        {
            get { return status; }
        }

        #endregion
    }
}

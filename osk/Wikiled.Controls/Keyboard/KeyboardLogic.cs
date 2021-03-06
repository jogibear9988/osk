﻿using System;
using System.ComponentModel;
using System.Windows.Browser;

namespace Wikiled.Controls.Keyboard
{
    /// <summary>
    /// Keyboard logic
    /// </summary>
    public class KeyboardLogic : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        [ScriptableMember]
        public event KeyPressed KeyPressed;

        public event EventHandler TextChanged;

        #endregion

        #region Variables
        private KeyboardDefinition activeDefinition;

        private string text;

        private bool visibleHelperControls = true;
        #endregion

        public KeyboardLogic()
        {
            KeyboardHandler = "keyboardHandler"; // default keyboard handler
            KeyboardLayouts.Instance.PropertyChanged += PropertyChange_Event;
            ChangeSubscription();
        }

        private void PropertyChange_Event(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            ChangeSubscription();
        }

        void activeDefinition_KeyPressed(object sender, KeyPressedEventArgs args)
        {
            // bubble event
            var keyPressedDelegate = KeyPressed;
            if (keyPressedDelegate != null)
            {
                keyPressedDelegate(this, args);
            }
        }

        private void ChangeSubscription()
        {
            // if we have subscribed before - unsubscribe
            if (activeDefinition != null)
            {
                activeDefinition.KeyPressed -= new KeyPressed(activeDefinition_KeyPressed);
            }
            activeDefinition = KeyboardLayouts.Instance.SelectedLayout;
            if (activeDefinition != null)
            {
                activeDefinition.KeyPressed += new KeyPressed(activeDefinition_KeyPressed);
            }
        }

        /// <summary>
        /// Read text from HTML textbox
        /// </summary>
        public void ReadText()
        {
            var instance = HtmlPage.Window.GetProperty("keyboardHandler") as ScriptObject;
            if (instance == null)
            {
                return;
            }
            var value = instance.Invoke("GetCurrentText", new object[] { }) as string;
            value = string.IsNullOrEmpty(value) ? string.Empty : value;
            Text = value;
        }

        /// <summary>
        /// Close button clinked handling
        /// Call external javascript to notify browser about closing event and update text in browser
        /// </summary>
        /// <param name="isOk"></param>
        public void CloseClicked(bool isOk)
        {
            var handler = TextChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
            if (string.IsNullOrEmpty(KeyboardHandler))
            {
                return;
            }
            // Get JavaScript object
            var instance = HtmlPage.Window.GetProperty(KeyboardHandler) as ScriptObject;
            if (instance == null)
            {
                return;
            }
            // fire close event
            HtmlPage.Document.Dispatcher.BeginInvoke(() => instance.Invoke("SetText", new object[] { isOk, this.Text }));
        }

        #region properties


        /// <summary>
        /// Keyboard handler javascript object name
        /// </summary>
        [ScriptableMember]
        public string KeyboardHandler { get; set; }

        /// <summary>
        /// Cuurent text
        /// </summary>
        [ScriptableMember]
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (text == value)
                {
                    return;
                }
                text = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Are helper controls visible
        /// </summary>
        [ScriptableMember]
        public bool VisibleHelperControls
        {
            get { return visibleHelperControls; }
            set
            {
                if (visibleHelperControls == value)
                {
                    return;
                }
                visibleHelperControls = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("VisibleHelperControls"));
                }
            }
        }

        #endregion
    }
}
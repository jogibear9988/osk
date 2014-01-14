using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Wikiled.Controls.Keyboard;
using Wikiled.Controls.UI;

namespace Wikiled.Controls.Keyboard
{
    /// <summary>
    /// Keyboard control
    /// </summary>
    public partial class KeyboardControl
    {
        public bool VisibleHelperControls
        {
            get { return (bool)GetValue(VisibleHelperControlsProperty); }
            set { SetValue(VisibleHelperControlsProperty, value); }
        }

        public static readonly DependencyProperty VisibleHelperControlsProperty =
            DependencyProperty.Register("VisibleHelperControls", typeof(bool), typeof(KeyboardControl), new PropertyMetadata(false));

        public TextBox CurrentTextBox { get; set; }
        
        public KeyboardControl()
        {
            InitializeComponent();

            CurrentTextBox = inputText;

            ((KeyStyleConverter) this.Resources["styleConverter"]).OwnerControl = this;
            
            Logic = new KeyboardLogic();
            Logic.KeyPressed += new KeyPressed(Logic_KeyPressed);
            this.DataContext = Logic;
            this.keyboardList.DataContext = KeyboardLayouts.Instance;
            this.allLayouts.DataContext = KeyboardLayouts.Instance;
        }

        #region event handlers

        private void closeClick(object sender, RoutedEventArgs e)
        {
            Logic.CloseClicked(sender == okButton);
        }

        /// <summary>
        /// Handle  key pressed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        void Logic_KeyPressed(object sender, KeyPressedEventArgs args)
        {
            string text = args.PressedKey ?? string.Empty;
            this.CurrentTextBox.Focus();

            if (args.Key is SpecialKey)
            {
                if (args.Key is SpaceKey)
                {
                    text = " ";
                }
                else if (args.Key is TabKey)
                {
                    text = "\t";
                }
                else if (args.Key is EnterKey)
                {
                    if (this.CurrentTextBox.AcceptsReturn)
                        text = "\r\n";
                    else
                    {
#if !SILVERLIGHT
                        //this.CurrentTextBox.KeyDown(sender, args);
                        this.CurrentTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        return;
#else
#endif
                    }
                }
                else if (args.Key is BackspaceKey)
                {
                    if (this.CurrentTextBox.Text.Length != 0 &&
                         this.CurrentTextBox.SelectionLength == 0 &&
                         this.CurrentTextBox.SelectionStart != 0)
                    {
                        this.CurrentTextBox.Select(this.CurrentTextBox.SelectionStart - 1, 1);
                    }
                    text = string.Empty;
                }
                else if (args.Key is DeleteKey)
                {
                    if (this.CurrentTextBox.Text.Length != 0 &&
                     this.CurrentTextBox.SelectionLength == 0 &&
                     this.CurrentTextBox.SelectionStart != this.CurrentTextBox.Text.Length)
                    {
                        this.CurrentTextBox.Select(this.CurrentTextBox.SelectionStart, 1);
                    }
                    text = string.Empty;
                }
            }
            // reuse selected text property to do text change 
            this.CurrentTextBox.SelectedText = text;
            this.CurrentTextBox.SelectionLength = 0;
            this.CurrentTextBox.SelectionStart = ++this.CurrentTextBox.SelectionStart;
            this.CurrentTextBox.Focus();
        } 
        #endregion


        public KeyboardLogic Logic { get; private set; }
    }
}
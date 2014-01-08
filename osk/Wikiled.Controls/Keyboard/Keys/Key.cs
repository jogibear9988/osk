using System;
using System.ComponentModel;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Wikiled.Controls.UI;

namespace Wikiled.Controls.Keyboard
{
    /// <summary>
    /// General key definition
    /// </summary>
    public class Key : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Key have been pressed
        /// </summary>
        public event EventHandler Pressed;
        /// <summary>
        /// Observable property have been changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Variables
        private bool isChecked;
        protected KeyboardStatus status;
        private string keyValue;
        private bool commandEnabled;
        private readonly DelegateCommand<string> command;

        #endregion

        public Key()
        {
            Style = "regularButton";
            command = new DelegateCommand<string>(CommandExecuted, CommandCanExecute);
        }

        /// <summary>
        /// Create key with given name
        /// </summary>
        /// <param name="name"></param>
        public Key(string name)
            : this()
        {
            Name = name;
        }

        #region Methods
        /// <summary>
        /// Handle property changed event
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// If command have been executed - fire pressed event
        /// </summary>
        /// <param name="parameter"></param>
        protected virtual void CommandExecuted(string parameter)
        {
            if (Pressed != null)
            {
                Pressed(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Can command be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CommandCanExecute(string parameter)
        {
            return true;
        }

        /// <summary>
        /// Register global keyboard status
        /// </summary>
        /// <param name="mainStatus"></param>
        internal virtual void RegisterStatusHolder(KeyboardStatus mainStatus)
        {
            this.status = mainStatus;
            status.PropertyChanged += new PropertyChangedEventHandler(status_PropertyChanged);
            UpdateStatus();
        }

        /// <summary>
        /// Handle keyboard status change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void status_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateStatus();
        }

        /// <summary>
        /// Update key status depending on global keyboard settigs
        /// </summary>
        protected virtual void UpdateStatus()
        {
            if (string.IsNullOrEmpty(RegularValue))
            {
                throw new KeyboardException("Can't update status as key don't have regular value");
            }

            if (status == null)
            {
                return;
            }

            string text = string.Empty;

            if (status.IsCtrlOn)
            {
                text = UpdateCtrl();
            }
            else if (status.IsAltOn) 
            {
                text = UpdateAlt();
            }
            else if (UpdateShift())
            {
                return;
            }

            text = String.IsNullOrEmpty(text) ? RegularValue : text;
            Value = status.IsCapital ?
                status.Culture.TextInfo.ToUpper(text) :
                status.Culture.TextInfo.ToLower(text);
        }

        /// <summary>
        /// Update CTRL
        /// </summary>
        /// <returns></returns>
        protected virtual string UpdateCtrl()
        {
            if (!string.IsNullOrEmpty(ControlValue))
            {
                return ControlValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// update alt
        /// </summary>
        /// <returns></returns>
        protected virtual string UpdateAlt()
        {
            if (!string.IsNullOrEmpty(AltValue))
            {
                return AltValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// Check for shift status
        /// </summary>
        /// <returns></returns>
        protected virtual bool UpdateShift()
        {
            if (status.IsShiftOn &&     // if shift selected
                !string.IsNullOrEmpty(ShiftValue))
            {
                Value = ShiftValue;
                return true;
            }
            return false;
        }

        #endregion

        #region properties

        /// <summary>
        /// Key name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Key value which will be displayed
        /// </summary>
        public virtual string Value
        {
            get { return keyValue; }
            set
            {
                if (value == keyValue)
                {
                    return;
                }
                keyValue = value;
                OnPropertyChanged("Value");
            }
        }

        /// <summary>
        /// This is normal key value
        /// </summary>
        public string RegularValue { get; set; }

        /// <summary>
        /// Value then shift is pressed
        /// </summary>
        public string ShiftValue { get; set; }

        /// <summary>
        /// Value when control is pressed
        /// </summary>
        public string ControlValue { get; set; }

        /// <summary>
        /// Value then Alt is pressed
        /// </summary>
        public string AltValue { get; set; }

        /// <summary>
        /// Keys style
        /// </summary>
        public string Style { get; set; }

        
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (value == isChecked)
                {
                    return;
                }
                isChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }

        public ICommand Command { get { return command; } }

        public bool CommandEnabled
        {
            get
            {
                return commandEnabled;
            }
            set
            {
                if (value != commandEnabled)
                {
                    commandEnabled = value;
                    OnPropertyChanged("CommandEnabled");
                    //Raise CanExecuteChanged on the command, for the 
                    //button to requery enablement, as in WPF
                    command.RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

    }
}

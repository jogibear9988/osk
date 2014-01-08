using System;
using System.ComponentModel;
using System.Globalization;
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
    /// <summary>
    /// Holds pressed key combinations
    /// </summary>
    public class KeyboardStatus : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        
        private bool isShiftOn;
        private bool isCtrlOn;
        private bool isCapsOn;
        private bool isAltOn;

        public KeyboardStatus(CultureInfo culture)
        {
            if (culture == null)
            {
                culture = CultureInfo.InvariantCulture;
            }
            this.Culture = culture;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler Handler = PropertyChanged;
            if (Handler != null)
            {
                Handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Is letters should be capital
        /// </summary>
        public bool IsCapital
        {
            get
            {
                return IsCapsOn ^ IsShiftOn;
            }
        }

        /// <summary>
        /// Is shift button on
        /// </summary>
        public bool IsShiftOn
        {
            get { return isShiftOn; }
            set
            {
                if (isShiftOn == value)
                {
                    return;
                }
                isShiftOn = value;
                OnPropertyChanged("IsShiftOn");
            }
        }

        /// <summary>
        /// is control on
        /// </summary>
        public bool IsCtrlOn
        {
            get { return isCtrlOn; }
            set
            {
                if (isCtrlOn == value)
                {
                    return;
                }
                isCtrlOn = value;
                OnPropertyChanged("IsCtrlOn");
            }
        }

        /// <summary>
        /// is control on
        /// </summary>
        public bool IsAltOn
        {
            get { return isAltOn; }
            set
            {
                if (isAltOn == value)
                {
                    return;
                }
                isAltOn = value;
                OnPropertyChanged("IsAltOn");
            }
        }

        /// <summary>
        /// is control on
        /// </summary>
        public bool IsCapsOn
        {
            get { return isCapsOn; }
            set
            {
                if (isCapsOn == value)
                {
                    return;
                }
                isCapsOn = value;
                OnPropertyChanged("IsCapsOn");
            }
        }

        /// <summary>
        /// Keyboard culture
        /// </summary>
        public CultureInfo Culture { get; private set; }

   
    }
}

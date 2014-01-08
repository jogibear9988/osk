using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Wikiled.Controls.Keyboard;

namespace Wikiled.Controls
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
            if (this.keyboard.Logic == null)
            {
                throw new KeyboardException("No active default keyboard");
            }
            HtmlPage.RegisterScriptableObject("silverKeyboard", this.keyboard.Logic); 
            this.keyboard.Logic.ReadText();
        }
        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for WindowLoginUI.xaml
    /// </summary>
    public partial class WindowLoginUI : Window
    {
        public WindowLoginUI()
        {
            InitializeComponent();
            SetPosition();
            Topmost = true;
        }
        private void SetPosition()
        {
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width - 60;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight - this.Height - 60;
        }
    }
}

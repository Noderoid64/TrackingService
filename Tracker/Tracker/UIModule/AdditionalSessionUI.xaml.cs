using System;
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

namespace Tracker.UIModule
{
    /// <summary>
    /// Interaction logic for AdditionalSessionUI.xaml
    /// </summary>
    public partial class AdditionalSessionUI : Window
    {
        public AdditionalSessionUI()
        {
            InitializeComponent();
            SetPosition();
            Topmost = true; // Send the window to the top of the screen
        }
        private void SetPosition()
        {            
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width - 60;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight - this.Height - 60;
        }
    }
}

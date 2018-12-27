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
    /// Interaction logic for WindowLoginUI.xaml
    /// </summary>
    public partial class WindowLoginUI : Window
    {
        public event EventHandler<WindowLoginEventArgs> ButtonLoginClick;

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
        public void ButtonLogin_Click(object sender, EventArgs e)
        {
            if(InputField.Text != "")
            {
                WindowLoginEventArgs wlea = new WindowLoginEventArgs(InputField.Text);
                ButtonLoginClick.Invoke(this, wlea);
            }
            else
            {
                InputField.Text = "Введите ключ";
                InputField.Foreground = Brushes.OrangeRed;
            }
            
        }
        public void SetError(string message)
        {
            InputField.Foreground = Brushes.PaleVioletRed;
            InputField.Text = message;
        }

        private void InputField_GotFocus(object sender, RoutedEventArgs e)
        {
            InputField.Text = "";
            InputField.Foreground = Brushes.White;
        }
    }
}

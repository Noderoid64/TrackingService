using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;
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
        private Timer timer;
        public EventHandler<AdditionalSessionUiEventArgs> SessionContinue;

        public AdditionalSessionUI()
        {
            InitializeComponent();
            setPosition();
            Topmost = true; // Send the window to the top of the screen
        }
        public void StartTimer()
        {
            ClockLabel.Text = 60.ToString();
            timer = new Timer(tick, null, 0, 1000);
        }
        public void SetTime(TimeSpan time)
        {
            TextBox.Text = "Вы хотите продлить смену на "
                + (time.Hours != 0 ? time.Hours.ToString() + " часов " : "")
                + (time.Minutes != 0 ? time.Minutes.ToString() + " минут " : "")
                + (time.Seconds != 0 ? time.Seconds.ToString() + " секунд " : "")
                + "?";
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            SessionContinue.Invoke(null, new AdditionalSessionUiEventArgs(true));
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            SessionContinue.Invoke(null, new AdditionalSessionUiEventArgs(false));
        }

        private void setPosition()
        {            
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth - this.Width - 60;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight - this.Height - 60;
        }
        
        private void tick(object sender)
        {
            int localInt = 61;
            this.Dispatcher.Invoke(() => localInt = int.Parse(ClockLabel.Text));
            if(localInt == 0)
            {
                timer.Dispose();
                ButtonNo_Click(ButtonNo, null);

            }
            else
            {
                this.Dispatcher.Invoke(() => ClockLabel.Text = (localInt - 1).ToString());
            }
        }

        
    }
}

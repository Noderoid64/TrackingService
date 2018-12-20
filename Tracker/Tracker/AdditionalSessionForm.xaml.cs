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
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Threading;

using Tracker.Tools.ApplicationSettings;
using Tracker.Tools.ConnectionModule;
using Tracker.Tools.TimerController;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for AdditionalSessionForm.xaml
    /// </summary>
    public partial class AdditionalSessionForm : Window
    {

        DispatcherTimer dispatcherTimer;
        NotifyIcon noty = new NotifyIcon();

        public AdditionalSessionForm()
        {
            Topmost = true;
            InitializeComponent();
            this.SetWindowPosition();

            this.setAdditionalTime();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

            ShowInTaskbar = false;
            noty.Icon = new System.Drawing.Icon("clock-icon.png.ico");
            noty.Text = "Tracker";
            noty.BalloonTipText = "Хотите продолжить?";
            noty.BalloonTipTitle = "Tracker";
            noty.ShowBalloonTip(2);

            
        }
        
        private void setAdditionalTime()
        {
            ApplicationSetting AS = ApplicationSetting.GetInstance();
            int hours = AS.Session.AdditionalTime / 3600;
            int minutes = AS.Session.AdditionalTime % 3600 / 60;
            int second = AS.Session.AdditionalTime % 60;
            TextBox.Text = "Вы хотите продлить смену ещё на " +
                (hours == 0? "" : hours + (hours % 10 == 1? " час" : (hours % 10 >= 2 && hours % 10 <= 4) ? " часа" : " часов"))
                + (minutes == 0 ? "" : minutes + (minutes % 10 == 1 ? " минуту" : (minutes % 10 >= 2 && minutes % 10 <= 4) ? " минуты" : " минут"))
                + (second == 0 ? "" : second + (second % 10 == 1 ? " секунду" : (second % 10 >= 2 && second % 10 <= 4) ? " секунды" : " секунд"));
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int second = int.Parse(ClockLabel.Text);
            if (second >= 1)
                ClockLabel.Text = (--second).ToString();
            else
            {
                ButtonNo_Click(null, null);
                Debug.Print("dispatcherTick");
            }

                
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            Debug.Print("dispatcher.Stop()");
            MainWindow mw = new MainWindow();
            mw.Show();
            
            this.Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            ConnectionModule CM = new DefaultConnectionModule();

            var sp = CM.StartSession(ApplicationSetting.GetInstance().Session.SessionKey);
            if (sp != null)
            {
                ApplicationSetting.GetInstance().Session.SessionKey = ApplicationSetting.GetInstance().Session.SessionKey;
                TimerController TC = new TimerController(sp);
                TC.SessionClose += CloseConnection;
                this.Hide();
            }
            else
            {
                //Графический вывод ошибки
                System.Windows.Forms.MessageBox.Show("Server did not allowed new session");
            }
        }
        private void CloseConnection(bool isReq)
        {
            Action action = () =>
            {
                ButtonNo_Click(null,null);
            };

            Dispatcher.Invoke(action);
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            
        }
    }
}

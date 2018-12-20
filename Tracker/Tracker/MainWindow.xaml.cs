using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Drawing;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Tracker.Tools.ApplicationSettings;
using Tracker.Tools.ConnectionModule;
using Tracker.Tools.TimerController;
using Tracker.Tools.ApplicationDiagnostic;
using Tracker.Tools.PingModule;

using System.Net.NetworkInformation;
using System.Diagnostics;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        NotifyIcon noty = new NotifyIcon();
        TimerController TC;
        public MainWindow()
        {
            AppDiagnostic.CheckOnInstanse();
            AppDiagnostic.SendIconToTray(noty);
            Topmost = true;
            InitializeComponent();
            this.SetWindowPosition();
            this.SetApplicationSettings("settings.ini");

            try
            {
                ShowInTaskbar = false;
                // m_notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                noty.Icon = new System.Drawing.Icon("clock-icon.png.ico");
                noty.Text = "Tracker";
                
                noty.BalloonTipText = "Введите ключ!";
                noty.BalloonTipTitle = "Tracker";
                noty.ShowBalloonTip(2);
            }
            catch(Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            

        }


        public void ButtonSingInClick(object sender, EventArgs ea)
        {
            ConnectionModule CM = new DefaultConnectionModule();

            string key = InputField.Text;
            if(key == "")
            {
                InputField.Foreground = new SolidColorBrush(Colors.Red);
                InputField.Text = "Введите ключ";
                return;
            }
            var sp = CM.StartSession(key);
            if (sp != null)
            {
                ApplicationSetting.GetInstance().Session.SessionKey = InputField.Text;
                TC = new TimerController(sp);
                TC.SessionClose += CallAdditionalSession;
                noty.BalloonTipText = "Вы авторизированы";
                noty.ShowBalloonTip(2);
                this.Hide();
            }
            else
            {
                InputField.Foreground = new SolidColorBrush(Colors.Red);
                InputField.Text = "Неверный ключ";
            }


        }

        public void CallAdditionalSession(bool IsAdditionalRequired)
        {
            if(IsAdditionalRequired)
            {
                Action action = () =>
                {
                    AdditionalSessionForm asf = new AdditionalSessionForm();
                    asf.Show();

                    this.Close();
                };

                Dispatcher.Invoke(action);
            }
            else
            {
                Action action = () =>
                {

                    TC.SessionClose -= CallAdditionalSession;
                    TC = null;
                    this.Show();
                };
                Dispatcher.Invoke(action);
            }
            
        }


        private void OnApplicationExit(object sender, EventArgs e)
        {
            
        }



        private void GotFocus(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.TextBox a)
            {
                a.Foreground = new SolidColorBrush(Colors.White);
                a.Text = "";
            }
        }
    }
}

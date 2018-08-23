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

        public AdditionalSessionForm()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
        }




        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            int second = int.Parse(ClockLabel.Text);
            if (second >= 1)
                ClockLabel.Text = (--second).ToString();
            else
            {
                ButtonNo_Click(null, null);
                dispatcherTimer.Stop();
            }

                
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
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
                MessageBox.Show("error");
            }
        }
        private void CloseConnection()
        {
            Action action = () =>
            {
                ButtonNo_Click(null,null);
            };

            Dispatcher.Invoke(action);
        }
    }
}

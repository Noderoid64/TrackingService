using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
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

using Tracker.Tools.ConnectionModule.WebAPI;
using Tracker.Tools.PostSender;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            this.SetWindowPosition();
            this.SetApplicationSettings("settings.ini");
        }

        

        public void ButtonSingInClick(object sender, EventArgs ea)
        {
            ConnectionModule CM = new DefaultConnectionModule();
            
            var sp = CM.StartSession(InputField.Text);
            if (sp != null)
            {
                MessageBox.Show(sp.show());
                ApplicationSetting.GetInstance().Session.SessionKey = InputField.Text;
                TimerController TC = new TimerController(sp);
                TC.SessionClose += CallAdditionalSession;
                this.Hide();
            }
            else
            {
                MessageBox.Show("error");
            }
        }

        public void CallAdditionalSession()
        {
            Action action = () =>
            {
                AdditionalSessionForm asf = new AdditionalSessionForm();
                asf.Show();
                this.Close();
            };

            Dispatcher.Invoke(action);
        }
        


    }
}

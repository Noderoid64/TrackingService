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
using MercuryLogger;
using MercuryLogger.Loggers;

namespace Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       public MainWindow()
        {
            Logger a = MainLogger.GetInstance();
            a.AddLogger(new FileLogger("./"));
            a.Log("works");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

using FloorPlanMap.Components.Objects.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TestLoad;

namespace MTest2 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            // Create an instance of the window named
            // by the current button.
            //Type type = this.GetType();
            //Assembly assembly = type.Assembly;
            Assembly assembly = Assembly.Load("TestLoad");
            var width = System.Windows.SystemParameters.PrimaryScreenWidth;
            var height = System.Windows.SystemParameters.PrimaryScreenHeight;
            TestLoad.MainWindow wintemp = null;
            for (int i=0; i<4; ++i) {
                TestLoad.MainWindow win = (TestLoad.MainWindow)assembly.CreateInstance("TestLoad.MainWindow");
                win.Left = (i % 2) * (width / 2);
                win.Top = Math.Floor((double)i / 2) * (height / 2);
                win.Width = width / 2;
                win.Height = height / 2;
                win.Show();
                wintemp = win;
            }

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(2000.0);
            timer.Tick += (object sender, EventArgs e) => {
                wintemp.Testx(
                    new CameraDevice() {
                        X = 425,
                        Y = 535,
                        Size = 1,
                        Angle = 0,
                    }
                );
            };
            timer.Start();

            this.Visibility = Visibility.Hidden;
        }
    }
}

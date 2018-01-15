using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FloorPlanMap.Components.Objects;

namespace TestLoad {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    internal class AnimationTick {
        private Timer t1 = new Timer();
        private static Random r = new Random();
        public AnimationTick(CameraObject target, double interval) {
            t1.Elapsed += (o, e) => {
                var seed = r.Next(0, 7);
                if (seed == 0) {
                    target.Angle = r.Next(0, 360);
                }  else if (seed == 1) {
                    target.Degree = r.NextDouble() * 4 + 0.5;
                } else if (seed == 2) {
                    target.Distance = r.NextDouble() * 8 + 1;
                } else if (seed == 3) {
                    target.Size = r.NextDouble() * 2 + 0.8;
                } else {
                    target.X = r.Next(50, 1100);
                    target.Y = r.Next(50, 500);
                }
            };
            t1.Interval = interval;
            t1.Start();
        }
    }

    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            FloorPlanMap.FloorPlanMapUnit unit = new FloorPlanMap.FloorPlanMapUnit();
            unit.MapSource = ".\\Resources\\FloorPlan.png";

            var obj1 = new FloorPlanMap.Components.Objects.CameraObject() {
                X = 100, Y = 100, Angle = 0, Size = 3, Distance = 1.6
            };
            unit.Objects.Add(obj1);
            //new AnimationTick(obj1, 500);

            //var obj2 = new FloorPlanMap.Components.Objects.CameraObject() {
            //    X = 300,
            //    Y = 300,
            //    Angle = 90,
            //};
            //unit.Objects.Add(obj2);
            //new AnimationTick(obj2, 800);

            //var obj3 = new FloorPlanMap.Components.Objects.CameraObject() {
            //    X = 500,
            //    Y = 500,
            //    Angle = 0,
            //};
            //unit.Objects.Add(obj3);
            //new AnimationTick(obj3, 400);

            //var obj4 = new FloorPlanMap.Components.Objects.CameraObject() {
            //    X = 400,
            //    Y = 400,
            //    Angle = 0,
            //};
            //unit.Objects.Add(obj4);
            //new AnimationTick(obj4, 300);

            //var obj5 = new FloorPlanMap.Components.Objects.CameraObject() {
            //    X = 600,
            //    Y = 400,
            //    Angle = 0,
            //};
            //unit.Objects.Add(obj5);
            //new AnimationTick(obj5, 400);

            this.Container.Children.Add(unit);



        }
    }
}

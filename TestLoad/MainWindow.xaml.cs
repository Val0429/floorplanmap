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
                var seed = r.Next(0, 9);
                if (seed == 0) {
                    target.Angle = r.Next(0, 360);
                }  else if (seed == 1) {
                    target.Degree = r.NextDouble() * 4 + 0.5;
                } else if (seed == 2) {
                    target.Distance = r.NextDouble() * 8 + 1;
                //} else if (seed == 3) {
                //    target.Size = r.NextDouble() * 2 + 0.8;
                } else if (seed == 4) {
                    target.Selected = !target.Selected;
                } else {
                    //target.X = r.Next(50, 1100);
                    //target.Y = r.Next(50, 500);
                }
            };
            t1.Interval = interval;
            t1.Start();
        }
    }

    internal class AnimationDroneTick {
        private Timer t1 = new Timer();
        private static Random r = new Random();
        public AnimationDroneTick(DroneObject target, double interval) {
            t1.Elapsed += (o, e) => {
                if (target.TakeOff == false) {
                    var sd = r.Next(0, 3);
                    if (sd == 0) target.TakeOff = true;
                    return;
                }

                var seed = r.Next(0, 10);
                if (seed == 0) {
                    target.Angle = r.Next(0, 360);
                } else if (seed == 1) {
                    target.Degree = r.NextDouble() * 4 + 0.5;
                } else if (seed == 2) {
                    target.Distance = r.NextDouble() * 4 + 1;
                } else if (seed == 3) {
                    target.Size = r.NextDouble() * 0.1 + 0.6;
                } else if (seed == 4) {
                    target.TakeOff = !target.TakeOff;
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

            //var obj1 = new FloorPlanMap.Components.Objects.CameraObject() {
            //    X = 220,
            //    Y = 220,
            //    Size = 1
            //};
            //unit.Objects.Add(obj1);

            //var obj2 = new FloorPlanMap.Components.Objects.CameraObject() {
            //    X = 200,
            //    Y = 200,
            //    Size = 1
            //};
            //unit.Objects.Add(obj2);

            //var t1 = new Timer();
            //t1.Elapsed += (object sender, ElapsedEventArgs e) => {
            //    obj1.Size = 2;
            //    t1.Stop();
            //};
            //t1.Interval = 1000;
            //t1.Start();


            var obj1 = new FloorPlanMap.Components.Objects.CameraObject() {
                X = 266,
                Y = 321,
                Angle = 0,
                Size = 1,
                Distance = 1.6
            };
            unit.Objects.Add(obj1);
            new AnimationTick(obj1, 500);

            var obj2 = new FloorPlanMap.Components.Objects.CameraObject() {
                X = 642,
                Y = 146,
                Size = 1,
                Angle = 90,
            };
            unit.Objects.Add(obj2);
            new AnimationTick(obj2, 800);

            var obj3 = new FloorPlanMap.Components.Objects.CameraObject() {
                X = 998,
                Y = 152,
                Size = 1,
                Angle = 0,
            };
            unit.Objects.Add(obj3);
            new AnimationTick(obj3, 400);

            var obj4 = new FloorPlanMap.Components.Objects.CameraObject() {
                X = 425,
                Y = 535,
                Size = 1,
                Angle = 0,
            };
            unit.Objects.Add(obj4);
            new AnimationTick(obj4, 300);

            var obj5 = new FloorPlanMap.Components.Objects.CameraObject() {
                X = 898,
                Y = 513,
                Size = 1,
                Angle = 0,
            };
            unit.Objects.Add(obj5);
            new AnimationTick(obj5, 400);


            var drone1 = new FloorPlanMap.Components.Objects.DroneObject() {
                X = 750,
                Y = 150,
                Size = 0.8,
                Angle = 45,
            };
            unit.Objects.Add(drone1);
            new AnimationDroneTick(drone1, 5000);

            var drone2 = new FloorPlanMap.Components.Objects.DroneObject() {
                X = 400,
                Y = 200,
                Size = 1,
                Angle = 0,
            };
            unit.Objects.Add(drone2);
            new AnimationDroneTick(drone2, 1000);

            var drone3 = new FloorPlanMap.Components.Objects.DroneObject() {
                X = 600,
                Y = 200,
                Size = 1,
                Angle = 0,
            };
            unit.Objects.Add(drone3);
            new AnimationDroneTick(drone3, 1500);


            this.Container.Children.Add(unit);



        }
    }
}

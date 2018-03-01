using FloorPlanMap.Components.Footprints;
using FloorPlanMap.Components.Objects.Devices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestLoad {
    /// <summary>
    /// Interaction logic for MainGrid.xaml
    /// </summary>
    public partial class MainGrid : UserControl {
        public MainGrid() {
            InitializeComponent();

            FloorPlanMap.FloorPlanMapUnit unit = new FloorPlanMap.FloorPlanMapUnit() {
                MapSource = ".\\Resources\\FloorPlan.png",
                MaxZoomLevel = 20,
            };

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


            var obj1 = new CameraDevice() {
                X = 266,
                Y = 321,
                Angle = 0,
                Size = 1,
                Distance = 1.6,
            };
            obj1.MouseDown += (object sender, MouseButtonEventArgs e) => {
                Console.WriteLine("Got1");
            };
            unit.Objects.Add(obj1);
            new AnimationTick(obj1, 500);

            var obj2 = new CameraDevice() {
                X = 642,
                Y = 146,
                Size = 1,
                Angle = 90,
            };
            obj2.MouseDown += (object sender, MouseButtonEventArgs e) => {
                Console.WriteLine("Got2");
            };
            unit.Objects.Add(obj2);
            new AnimationTick(obj2, 800);

            var obj3 = new CameraDevice() {
                X = 998,
                Y = 152,
                Size = 1,
                Angle = 0,
            };
            obj3.MouseDown += (object sender, MouseButtonEventArgs e) => {
                Console.WriteLine("Got3");
            };
            unit.Objects.Add(obj3);
            new AnimationTick(obj3, 400);

            var obj4 = new CameraDevice() {
                X = 425,
                Y = 535,
                Size = 1,
                Angle = 0,
            };
            obj4.MouseDown += (object sender, MouseButtonEventArgs e) => {
                Console.WriteLine("Got4");
            };
            unit.Objects.Add(obj4);
            new AnimationTick(obj4, 300);

            var obj5 = new CameraDevice() {
                X = 898,
                Y = 513,
                Size = 1,
                Angle = 0,
            };
            obj5.MouseDown += (object sender, MouseButtonEventArgs e) => {
                Console.WriteLine("Got5");
            };
            unit.Objects.Add(obj5);
            new AnimationTick(obj5, 400);

            var drone1 = new DroneDevice() {
                X = 750,
                Y = 150,
                Size = 0.8,
                Angle = 45,
                AnimationDurationX = 5000,
                AnimationDurationY = 5000,
                FootprintType = new NormalFootprint() { Color = (Color)ColorConverter.ConvertFromString("Orange") },
                FootprintDuration = TimeSpan.FromMilliseconds(3000)
            };
            unit.Objects.Add(drone1);
            new AnimationDroneTick(drone1, 5000);

            var drone2 = new DroneDevice() {
                X = 400,
                Y = 200,
                Size = 1,
                Angle = 0,
                AnimationDurationX = 1000,
                AnimationDurationY = 1000,
                FootprintType = new NormalFootprint() { Color = (Color)ColorConverter.ConvertFromString("LemonChiffon") },
                FootprintDuration = TimeSpan.FromMilliseconds(3000),
            };
            unit.Objects.Add(drone2);
            new AnimationDroneTick(drone2, 1000);

            var drone3 = new DroneDevice() {
                X = 600,
                Y = 200,
                Size = 1,
                Angle = 0,
                AnimationDurationX = 1500,
                AnimationDurationY = 1500,
                FootprintType = new NormalFootprint() { Color = (Color)ColorConverter.ConvertFromString("Cyan") },
                FootprintDuration = TimeSpan.FromMilliseconds(3000),
            };
            unit.Objects.Add(drone3);
            var adt = new AnimationDroneTick(drone3, 1500);

            //var t = new Timer();
            //t.Elapsed += (object sender, ElapsedEventArgs e) => {
            //    t.Stop();
            //    adt.Stop();
            //    unit.Dispatcher.BeginInvoke(new Action(
            //        () => unit.Objects.Remove(drone3)
            //        ));

            //};
            //t.Interval = 20000;
            //t.Start();


            this.Container.Children.Add(unit);

        }
    }
}

using FloorPlanMap.Components.Footprints;
using FloorPlanMap.Components.Objects.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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
                MapSource = @"Resources\FloorPlan.png",
                MaxZoomLevel = 20,
            };

            var drone1 = new DroneDevice() {
                X = 1195,
                Y = 367,
                Size = 0.8,
                Angle = 90,
                Template = (ControlTemplate)this.FindResource("MyRobot"),
                AnimationDurationX = 5000,
                AnimationDurationY = 5000,
                Degree = 2,
                Distance = 1,
                FootprintType = new NormalFootprint() { Color = (Color)ColorConverter.ConvertFromString("Orange"), Size = 1 },
                FootprintDuration = TimeSpan.FromMilliseconds(60000)
            };
            unit.Objects.Add(drone1);
            //new AnimationDroneTick(drone1, 5000);

            double rate = 5;

            Observable.Timer(TimeSpan.FromMilliseconds(1000))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 500 * rate;
                        drone1.AnimationDurationY = 500 * rate;
                        drone1.X = 1054;
                        drone1.Y = 368;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(400 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 100 * rate;
                        drone1.AnimationDurationY = 100 * rate;
                        drone1.Size = 0.4;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(100 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.TakeOff = true;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(30 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Distance = 3;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(200 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 200 * rate;
                        drone1.AnimationDurationY = 200 * rate;
                        drone1.X = 967;
                        drone1.Y = 369;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(200 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 100 * rate;
                        drone1.AnimationDurationY = 100 * rate;
                        drone1.X = 947;
                        drone1.Y = 334;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(100 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 1500 * rate;
                        drone1.AnimationDurationY = 1500 * rate;
                        drone1.Distance = 10;
                        drone1.X = 568;
                        drone1.Y = 336;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(1050 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Distance = 3;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(400 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationAngle = 50 * rate;
                        drone1.Angle = 10;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 200 * rate;
                        drone1.AnimationDurationY = 200 * rate;
                        drone1.X = 548;
                        drone1.Y = 450;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(150 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 100 * rate;
                        drone1.AnimationDurationY = 100 * rate;
                        drone1.X = 505;
                        drone1.Y = 434;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Angle = 180;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 150 * rate;
                        drone1.AnimationDurationY = 150 * rate;
                        drone1.X = 506;
                        drone1.Y = 341;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(100 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Angle = 90;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 300 * rate;
                        drone1.AnimationDurationY = 300 * rate;
                        drone1.X = 402;
                        drone1.Y = 337;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(250 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Angle = 180;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 1000 * rate;
                        drone1.AnimationDurationY = 1000 * rate;
                        drone1.X = 400;
                        drone1.Y = 60;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(950 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Angle = 90;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.TakeOff = false;
                    });
                })
                /// go back
                .Delay(TimeSpan.FromMilliseconds(500 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.TakeOff = true;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(100 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Angle = 0;
                        ((NormalFootprint)drone1.FootprintType).Color = (Color)ColorConverter.ConvertFromString("LemonChiffon");
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 1000 * rate;
                        drone1.AnimationDurationY = 1000 * rate;
                        drone1.X = 402;
                        drone1.Y = 337;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(950 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Angle = -90;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 300 * rate;
                        drone1.AnimationDurationY = 300 * rate;
                        drone1.X = 506;
                        drone1.Y = 341;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(250 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Angle = 0;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 150 * rate;
                        drone1.AnimationDurationY = 150 * rate;
                        drone1.X = 505;
                        drone1.Y = 434;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(100 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 100 * rate;
                        drone1.AnimationDurationY = 100 * rate;
                        drone1.X = 548;
                        drone1.Y = 450;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Angle = -190;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 200 * rate;
                        drone1.AnimationDurationY = 200 * rate;
                        drone1.X = 568;
                        drone1.Y = 336;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(150 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Angle = -90;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 1500 * rate;
                        drone1.AnimationDurationY = 1500 * rate;
                        drone1.X = 947;
                        drone1.Y = 334;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(1500 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 100 * rate;
                        drone1.AnimationDurationY = 100 * rate;
                        drone1.X = 967;
                        drone1.Y = 369;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(100 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 200 * rate;
                        drone1.AnimationDurationY = 200 * rate;
                        drone1.X = 1054;
                        drone1.Y = 368;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(150 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.Size = 0.8;
                    });
                })
                .Delay(TimeSpan.FromMilliseconds(50 * rate))
                .Do((x) => {
                    drone1.SetAsync(() => {
                        drone1.AnimationDurationX = 500 * rate;
                        drone1.AnimationDurationY = 500 * rate;
                        drone1.X = 1195;
                        drone1.Y = 367;
                    });
                })
                .Subscribe();

            //_subscription = Observable.CombineLatest(_sjXChanged, _sjYChanged)
            //    .Select((o) => {
            //        return Observable.Timer(TimeSpan.FromMilliseconds(20))
            //            .Select((t) => o);
            //    })
            //    .Switch()
            //    .Subscribe(X => HandleXYChanged(X[0], X[1]));

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

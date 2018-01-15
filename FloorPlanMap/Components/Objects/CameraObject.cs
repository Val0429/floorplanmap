using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FloorPlanMap.Components.Objects {

    public class CameraObject : Control {
        public CameraObject() {
            /// Disable Animation on start
            Animation = false;
            base.Loaded += (object sender, RoutedEventArgs e) => {
                Animation = true;
            };
            //base.Initialized += (object sender, EventArgs e) => {
            //    Animation = true;
            //};
        }

        static CameraObject() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CameraObject), new FrameworkPropertyMetadata(typeof(CameraObject)));
        }

        #region "Dependency Properties"

        #region "Animation"
        public static readonly DependencyProperty AnimationProperty = DependencyProperty.Register(
                "Animation", typeof(bool), typeof(CameraObject),
                new FrameworkPropertyMetadata(false));
        [Description("Animation on / off."), Category("Source")]
        public bool Animation {
            get { return (bool)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }
        #endregion "Animation"

        #region "X"
        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
                "X", typeof(double), typeof(CameraObject),
                new FrameworkPropertyMetadata(0.0));
        [Description("Camera position X."), Category("Source")]
        public double X {
            get { return (double)GetValue(XProperty); }
            set {
                this.Dispatcher.BeginInvoke(new Action(() => {
                    var duration = Animation ? 800 : 0;
                    this.BeginAnimation(CameraObject.XProperty, new DoubleAnimation(value, TimeSpan.FromMilliseconds(duration)));
                }));
            }
        }
        #endregion "X"

        #region "Y"
        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
                "Y", typeof(double), typeof(CameraObject),
                new FrameworkPropertyMetadata(0.0));
        [Description("Camera position Y."), Category("Source")]
        public double Y {
            get { return (double)GetValue(YProperty); }
            set {
                this.Dispatcher.BeginInvoke(new Action(() => {
                    var duration = Animation ? 800 : 0;
                    this.BeginAnimation(CameraObject.YProperty, new DoubleAnimation(value, TimeSpan.FromMilliseconds(duration)));
                }));
            }
        }
        #endregion "Y"

        #region "Angle"
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
                "Angle", typeof(double), typeof(CameraObject),
                new FrameworkPropertyMetadata(0.0));
        [Description("Camera view angle."), Category("Source")]
        public double Angle {
            get { return (double)GetValue(AngleProperty); }
            set {
                this.Dispatcher.BeginInvoke(new Action(() => {
                    var duration = Animation ? 1500 : 0;
                    this.BeginAnimation(CameraObject.AngleProperty, new DoubleAnimation(value, TimeSpan.FromMilliseconds(duration)));
                }));
            }
        }
        #endregion "Angle"

        #region "Degree"
        public static readonly DependencyProperty DegreeProperty = DependencyProperty.Register(
                "Degree", typeof(double), typeof(CameraObject),
                new FrameworkPropertyMetadata(1.5));
        [Description("Camera view degree (wideness)."), Category("Source")]
        public double Degree {
            get { return (double)GetValue(DegreeProperty); }
            set {
                this.Dispatcher.BeginInvoke(new Action(() => {
                    var duration = Animation ? 1500 : 0;
                    this.BeginAnimation(CameraObject.DegreeProperty, new DoubleAnimation(value, TimeSpan.FromMilliseconds(duration)));
                }));
            }
        }
        #endregion "Degree"

        #region "Distance"
        public static readonly DependencyProperty DistanceProperty = DependencyProperty.Register(
                "Distance", typeof(double), typeof(CameraObject),
                new FrameworkPropertyMetadata(3.0));
        [Description("Camera view light distance."), Category("Source")]
        public double Distance {
            get { return (double)GetValue(DistanceProperty); }
            set {
                this.Dispatcher.BeginInvoke(new Action(() => {
                    var duration = Animation ? 600 : 0;
                    this.BeginAnimation(CameraObject.DistanceProperty, new DoubleAnimation(value, TimeSpan.FromMilliseconds(duration)));
                }));
            }
        }
        #endregion "Distance"

        #region "Size"
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
                "Size", typeof(double), typeof(CameraObject),
                new FrameworkPropertyMetadata(1.5));
        [Description("Camera size."), Category("Source")]
        public double Size {
            get { return (double)GetValue(SizeProperty); }
            set {
                this.Dispatcher.BeginInvoke(new Action(() => {
                    var duration = Animation ? 600 : 0;
                    this.BeginAnimation(CameraObject.SizeProperty, new DoubleAnimation(value, TimeSpan.FromMilliseconds(duration)));
                }));
            }
        }
        #endregion "Size"


        #endregion "Dependency Properties"
    }

}

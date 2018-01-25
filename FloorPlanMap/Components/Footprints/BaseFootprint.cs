using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FloorPlanMap.Components.Footprints {
    public class BaseFootprint : BaseComponent {
        private IDisposable _subscription;
        public BaseFootprint() {
            BaseZIndex = -1000;
            AnimationDurationX = 0;
            AnimationDurationY = 0;
            AnimationDurationAngle = 0;

            base.Loaded += (object sender, RoutedEventArgs e) => {
                VisualHeight = (double)FindResource("VisualHeight");

                _subscription = Observable.CombineLatest(_sjTXChanged, _sjTYChanged)
                    .Select((o) => {
                        return Observable.Timer(TimeSpan.FromMilliseconds(1))
                            .Select((t) => o);
                    })
                    .Switch()
                    .Subscribe(X => HandleXYChanged(X[0], X[1]));
            };

            base.Unloaded += (object sender, RoutedEventArgs e) => {
                _subscription.Dispose();
            };
        }

        #region "Normal Properties"
        #region "VisualHeight"
        private double _visualHeight = 0;
        public double VisualHeight {
            get { return _visualHeight; }
            private set { _visualHeight = value; }
        }
        #endregion "VisualHeight"
        #endregion "Normal Properties"

        #region "Handle X / Y / TargetX / TargetY Changed For Fluently CalculateLength"
        private Subject<double> _sjXChanged = new Subject<double>();
        private Subject<double> _sjYChanged = new Subject<double>();
        private Subject<double> _sjTXChanged = new Subject<double>();
        private Subject<double> _sjTYChanged = new Subject<double>();
        private void HandleXYChanged(double x, double y) {
            // Calculate Length
            this.SetAsync(CalculateLength);
        }
        #endregion "Handle X / Y / TargetX / TargetY Changed For Fluently CalculateLength"

        #region "Dependency Properties"

        #region "TargetX"
        public static readonly DependencyProperty TargetXProperty = DependencyProperty.Register(
                "TargetX", typeof(double), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(0.0,
                    new PropertyChangedCallback(OnTargetXChanged)
                    ));
        [Description("Footprint Target X."), Category("Source")]
        public double TargetX {
            get { return (double)this.GetDispatcherValue(TargetXProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(TargetXProperty, value, AnimationDurationX); }
        }
        private static void OnTargetXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BaseFootprint vm = d as BaseFootprint;
            if ((double)e.OldValue == 0) vm.CalculateLength();
            vm._sjTXChanged.OnNext((double)e.NewValue);
        }
        #endregion "TargetX"

        #region "TargetY"
        public static readonly DependencyProperty TargetYProperty = DependencyProperty.Register(
                "TargetY", typeof(double), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(0.0,
                    new PropertyChangedCallback(OnTargetYChanged)
                    ));
        [Description("Footprint Target Y."), Category("Source")]
        public double TargetY {
            get { return (double)this.GetDispatcherValue(TargetYProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(TargetYProperty, value, AnimationDurationY); }
        }
        private static void OnTargetYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BaseFootprint vm = d as BaseFootprint;
            if ((double)e.OldValue == 0) vm.CalculateLength();
            vm._sjTYChanged.OnNext((double)e.NewValue);
        }
        #endregion "TargetY"

        #region "Size"
        public static readonly new DependencyProperty SizeProperty = DependencyProperty.Register(
                "Size", typeof(double), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(1.0,
                    new PropertyChangedCallback(OnSizeChanged)
                    ));
        [Description("Object size."), Category("Source")]
        public override double Size {
            get { return (double)this.GetDispatcherValue(SizeProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(SizeProperty, value, 600); }
        }
        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BaseFootprint vm = d as BaseFootprint;
            vm.ZIndex = (double)e.NewValue * 5 + vm.BaseZIndex;
            // Calculate Length
            vm.CalculateLength();
        }
        #endregion "Size"

        #region "StartOpacity"
        public static readonly DependencyProperty StartOpacityProperty = DependencyProperty.Register(
                "StartOpacity", typeof(double), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(1.0,
                    new PropertyChangedCallback(OnStartOpacityChanged)
                    ));
        [Description("Footprint Start Opacity."), Category("Source")]
        public double StartOpacity {
            get { return (double)this.GetDispatcherValue(StartOpacityProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(StartOpacityProperty, value, 800); }
        }
        private static void OnStartOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            NormalFootprint vm = d as NormalFootprint;
            byte opacity = (byte)((double)e.NewValue * 255);
            vm.StartOpacityColor = Color.FromArgb(opacity, 0, 0, 0);
        }
        #endregion "StartOpacity"

        #region "StartOpacityPoint"
        public static readonly DependencyProperty StartOpacityPointProperty = DependencyProperty.Register(
                "StartOpacityPoint", typeof(double), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(1.0));
        [Description("Footprint Start Opacity."), Category("Source")]
        public double StartOpacityPoint {
            get { return (double)this.GetDispatcherValue(StartOpacityPointProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(StartOpacityPointProperty, value, 800); }
        }
        #endregion "StartOpacityPoint"

        #region "TargetOpacity"
        public static readonly DependencyProperty TargetOpacityProperty = DependencyProperty.Register(
                "TargetOpacity", typeof(double), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(1.0,
                    new PropertyChangedCallback(OnTargetOpacityChanged)
                    ));
        [Description("Footprint Target Opacity."), Category("Source")]
        public double TargetOpacity {
            get { return (double)this.GetDispatcherValue(TargetOpacityProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(TargetOpacityProperty, value, 800); }
        }
        private static void OnTargetOpacityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BaseFootprint vm = d as BaseFootprint;
            byte opacity = (byte)((double)e.NewValue * 255);
            vm.TargetOpacityColor = Color.FromArgb(opacity, 0, 0, 0);
        }
        #endregion "TargetOpacity"

        #region "Private Properties"
        #region "Length"
        private static readonly DependencyProperty LengthProperty = DependencyProperty.Register(
                "Length", typeof(double), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(0.0));
        [Description("Length."), Category("Source")]
        public double Length {
            get { return (double)this.GetDispatcherValue(LengthProperty); }
            private set { this.SetDispatcherValue(LengthProperty, value); }
        }
        #endregion "Length"

        #region "Angle"
        private static readonly new DependencyProperty AngleProperty = DependencyProperty.Register(
                "Angle", typeof(double), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(0.0));
        [Description("Footstep angle."), Category("Source")]
        public new double Angle {
            get { return (double)this.GetDispatcherValue(AngleProperty); }
            private set { this.SetDispatcherAnimationValue<DoubleAnimation>(AngleProperty, value, AnimationDurationAngle); }
        }
        #endregion "Angle"

        #region "StartOpacityColor"
        private static readonly DependencyProperty StartOpacityColorProperty = DependencyProperty.Register(
                "StartOpacityColor", typeof(Color), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(Color.FromRgb(0, 0, 0)));
        [Description("Footprint Start Opacity Color."), Category("Source")]
        public Color StartOpacityColor {
            get { return (Color)this.GetDispatcherValue(StartOpacityColorProperty); }
            private set { this.SetDispatcherValue(StartOpacityColorProperty, value); }
        }
        #endregion "StartOpacityColor"

        #region "TargetOpacityColor"
        private static readonly DependencyProperty TargetOpacityColorProperty = DependencyProperty.Register(
                "TargetOpacityColor", typeof(Color), typeof(BaseFootprint),
                new FrameworkPropertyMetadata(Color.FromRgb(0, 0, 0)));
        [Description("Footprint Target Opacity Color."), Category("Source")]
        public Color TargetOpacityColor {
            get { return (Color)this.GetDispatcherValue(TargetOpacityColorProperty); }
            private set { this.SetDispatcherValue(TargetOpacityColorProperty, value); }
        }
        #endregion "TargetOpacityColor"

        #endregion "Private Properties"

        #endregion "Dependency Properties"

        #region "Private Helper"
        protected void CalculateLength() {
            var dx = TargetX - X;
            var dy = TargetY - Y;
            // Length
            double length = Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2)) / Size;
            Length = length;
            // Angle
            var theta = Math.Atan2(dy, dx);
            theta *= 180 / Math.PI;
            theta -= 90;
            if (theta < 0) theta += 360;
            Angle = theta;
        }
        #endregion "Private Helper"

        #region "Public Helper"
        public bool AngleMatches(double x, double y, double targetx, double targety) {
            var dx = TargetX - X;
            var dy = TargetY - Y;
            var dtx = targetx - x;
            var dty = targety - y;

            var val1 = Math.Atan2(dy, dx);
            var val2 = Math.Atan2(dty, dtx);
            if (val1 == 0 && val2 == 0) return true;
            if (Math.Abs((val1 - val2) / (val1 == 0 ? val2 : val1)) < 0.001) return true;
            else return false;
        }
        #endregion "Public Helper"
    }
}

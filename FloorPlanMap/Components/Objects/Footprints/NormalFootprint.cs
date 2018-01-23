using FloorPlanMap.Components.Objects;
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

namespace FloorPlanMap.Components.Footprints {
    public class NormalFootprint : BaseObject {
        public NormalFootprint() {
            BaseZIndex = -1000;
        }
        static NormalFootprint() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NormalFootprint), new FrameworkPropertyMetadata(typeof(NormalFootprint)));
        }

        #region "Dependency Properties"

        #region "TargetX"
        public static readonly DependencyProperty TargetXProperty = DependencyProperty.Register(
                "TargetX", typeof(double), typeof(NormalFootprint),
                new FrameworkPropertyMetadata(0.0,
                    new PropertyChangedCallback(OnTargetXChanged)
                    ));
        [Description("Footprint Target X."), Category("Source")]
        public double TargetX {
            get { return (double)this.GetDispatcherValue(TargetXProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(TargetXProperty, value, 800); }
        }
        private static void OnTargetXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            NormalFootprint vm = d as NormalFootprint;
            // Calculate Length
            vm.CalculateLength();
        }
        #endregion "TargetX"

        #region "TargetY"
        public static readonly DependencyProperty TargetYProperty = DependencyProperty.Register(
                "TargetY", typeof(double), typeof(NormalFootprint),
                new FrameworkPropertyMetadata(0.0,
                    new PropertyChangedCallback(OnTargetYChanged)
                    ));
        [Description("Footprint Target Y."), Category("Source")]
        public double TargetY {
            get { return (double)this.GetDispatcherValue(TargetYProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(TargetYProperty, value, 800); }
        }
        private static void OnTargetYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            NormalFootprint vm = d as NormalFootprint;
            // Calculate Length
            vm.CalculateLength();
        }
        #endregion "TargetY"

        #region "Length"
        public static readonly DependencyProperty LengthProperty = DependencyProperty.Register(
                "Length", typeof(double), typeof(NormalFootprint),
                new FrameworkPropertyMetadata(0.0));
        [Description("Length."), Category("Source")]
        public double Length {
            get { return (double)this.GetDispatcherValue(LengthProperty); }
            private set { SetValue(LengthProperty, value); }
        }
        #endregion "Length"

        #region "Angle"
        public static readonly new DependencyProperty AngleProperty = DependencyProperty.Register(
                "Angle", typeof(double), typeof(NormalFootprint),
                new FrameworkPropertyMetadata(0.0));
        [Description("Footstep angle."), Category("Source")]
        public new double Angle {
            get { return (double)this.GetDispatcherValue(AngleProperty); }
            private set { this.SetDispatcherAnimationValue<DoubleAnimation>(AngleProperty, value, 600); }
        }
        #endregion "Angle"

        #region "Size"
        public static readonly new DependencyProperty SizeProperty = DependencyProperty.Register(
                "Size", typeof(double), typeof(NormalFootprint),
                new FrameworkPropertyMetadata(1.0,
                    new PropertyChangedCallback(OnSizeChanged)
                    ));
        [Description("Object size."), Category("Source")]
        public override double Size {
            get { return (double)this.GetDispatcherValue(SizeProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(SizeProperty, value, 600); }
        }
        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            NormalFootprint vm = d as NormalFootprint;
            vm.ZIndex = (double)e.NewValue * 5 + vm.BaseZIndex;
            // Calculate Length
            vm.CalculateLength();
        }
        #endregion "Size"

        #endregion "Dependency Properties"

        #region "Private Helper"
        private void CalculateLength() {
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
            Console.WriteLine("{0} {1}", Math.Atan(dy / dx), theta);
            Angle = theta;
        }
        #endregion "Private Helper"
    }
}

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
            double length = Math.Sqrt( Math.Pow(vm.TargetX - vm.X, 2) + Math.Pow(vm.TargetY - vm.Y, 2) );
            vm.Length = length;
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
            double length = Math.Sqrt(Math.Pow(vm.TargetX - vm.X, 2) + Math.Pow(vm.TargetY - vm.Y, 2));
            vm.Length = length;
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
        //public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
        //        "Angle", typeof(double), typeof(NormalFootprint),
        //        new FrameworkPropertyMetadata(0.0));
        //[Description("Camera view angle."), Category("Source")]
        //public double Angle {
        //    get { return (double)this.GetDispatcherValue(AngleProperty); }
        //    set { this.SetDispatcherAnimationValue<DoubleAnimation>(AngleProperty, value, 1500); }
        //}
        #endregion "Angle"

        #region "Size"
        //public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
        //        "Size", typeof(double), typeof(NormalFootprint),
        //        new FrameworkPropertyMetadata(1.5,
        //            new PropertyChangedCallback(OnSizeChanged)
        //            ));
        //[Description("Object size."), Category("Source")]
        //public double Size {
        //    get { return (double)this.GetDispatcherValue(SizeProperty); }
        //    set { this.SetDispatcherAnimationValue<DoubleAnimation>(SizeProperty, value, 600); }
        //}
        //private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        //    NormalFootprint vm = d as NormalFootprint;
        //    vm.ZIndex = (double)e.NewValue * 5 + vm.BaseZIndex;
        //}
        #endregion "Size"

        #endregion "Dependency Properties"
    }
}

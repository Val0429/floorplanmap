using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace FloorPlanMap.Components {
    public class BaseComponent : Control {
        protected double BaseZIndex = 0;

        protected BaseComponent() {
            /// Disable Animation on start
            Animation = false;
            base.Loaded += (object sender, RoutedEventArgs e) => {
                Animation = true;
            };
        }

        #region "Get / Set Helper"
        protected void SetDispatcherValue(DependencyProperty dp, object value) {
            this.Dispatcher.BeginInvoke(new Action(() => SetValue(dp, value)), System.Windows.Threading.DispatcherPriority.Normal);
        }
        protected void SetDispatcherAnimationValue<T>(DependencyProperty dp, object value, double duration) where T : AnimationTimeline {
            this.Dispatcher.BeginInvoke(new Action(() => {
                var rd = Animation ? duration : 0;
                var instance = Activator.CreateInstance(typeof(T), new object[] { value, (Duration)TimeSpan.FromMilliseconds(rd) }) as T;
                this.BeginAnimation(dp, instance);
            }), System.Windows.Threading.DispatcherPriority.Normal);
        }
        protected object GetDispatcherValue(DependencyProperty dp) {
            object result = null;
            try { result = this.Dispatcher.Invoke(() => GetValue(dp), System.Windows.Threading.DispatcherPriority.Normal); }
            catch {
                if (Application.Current != null) Application.Current.Shutdown();     /// Exit Gracefully
            }
            return result;
        }
        #endregion "Get / Set Helper"

        #region "Dependency Properties"

        #region "Animation"
        public static readonly DependencyProperty AnimationProperty = DependencyProperty.Register(
                "Animation", typeof(bool), typeof(BaseComponent),
                new FrameworkPropertyMetadata(false));
        [Description("Animation on / off."), Category("Source")]
        public bool Animation {
            get { return (bool)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }
        #endregion "Animation"

        #region "X"
        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
                "X", typeof(double), typeof(BaseComponent),
                new FrameworkPropertyMetadata(0.0,
                    new PropertyChangedCallback(OnXChanged)
                    ));
        [Description("Component position X."), Category("Source")]
        public double X {
            get { return (double)this.GetDispatcherValue(XProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(XProperty, value, 800); }
        }
        private static void OnXChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BaseComponent vm = d as BaseComponent;
            if (vm.OnXChangedEvent != null) vm.OnXChangedEvent(d, e);
        }
        public event PropertyChangedCallback OnXChangedEvent;
        #endregion "X"

        #region "Y"
        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
                "Y", typeof(double), typeof(BaseComponent),
                new FrameworkPropertyMetadata(0.0,
                    new PropertyChangedCallback(OnYChanged)
                    ));
        [Description("Component position Y."), Category("Source")]
        public double Y {
            get { return (double)this.GetDispatcherValue(YProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(YProperty, value, 800); }
        }
        private static void OnYChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BaseComponent vm = d as BaseComponent;
            if (vm.OnYChangedEvent != null) vm.OnYChangedEvent(d, e);
        }
        public event PropertyChangedCallback OnYChangedEvent;
        #endregion "Y"

        #region "Angle"
        public static readonly DependencyProperty AngleProperty = DependencyProperty.Register(
                "Angle", typeof(double), typeof(BaseComponent),
                new FrameworkPropertyMetadata(0.0));
        [Description("Component angle."), Category("Source")]
        public virtual double Angle {
            get { return (double)this.GetDispatcherValue(AngleProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(AngleProperty, value, 1500); }
        }
        #endregion "Angle"

        #region "Size"
        public static readonly DependencyProperty SizeProperty = DependencyProperty.Register(
                "Size", typeof(double), typeof(BaseComponent),
                new FrameworkPropertyMetadata(1.5,
                    new PropertyChangedCallback(OnSizeChanged)
                    ));
        [Description("Component size."), Category("Source")]
        public virtual double Size {
            get { return (double)this.GetDispatcherValue(SizeProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(SizeProperty, value, 600); }
        }
        private static void OnSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            BaseComponent vm = d as BaseComponent;
            vm.ZIndex = (double)e.NewValue * 5 + vm.BaseZIndex;
        }
        #endregion "Size"

        #region "ZIndex"
        public static readonly DependencyProperty ZIndexProperty = DependencyProperty.Register(
                "ZIndex", typeof(double), typeof(BaseComponent),
                new FrameworkPropertyMetadata(0.0));
        [Description("Component ZIndex."), Category("Source")]
        public double ZIndex {
            get { return (double)this.GetDispatcherValue(ZIndexProperty); }
            internal set { this.SetDispatcherValue(ZIndexProperty, value); }
        }
        #endregion "ZIndex"

        #endregion "Dependency Properties"
    }
}

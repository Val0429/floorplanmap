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

namespace FloorPlanMap.Components.Objects.Devices {
    [TemplateVisualState(Name = "Station", GroupName = "TakeOffStates")]
    [TemplateVisualState(Name = "TakeOff", GroupName = "TakeOffStates")]
    public class DroneDevice : BaseVideoDevice {
        public DroneDevice() {
            BaseZIndex = 10000;
            base.Loaded += (object sender, RoutedEventArgs e) => {
                VisualStateManager.GoToState(this, "Station", false);
            };
        }
        static DroneDevice() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DroneDevice), new FrameworkPropertyMetadata(typeof(DroneDevice)));
        }

        #region "Dependency Properties"

        #region "TakeOff"
        public static readonly DependencyProperty TakeOffProperty = DependencyProperty.Register(
            "TakeOff", typeof(bool), typeof(DroneDevice),
            new FrameworkPropertyMetadata(false,
                new PropertyChangedCallback(OnTakeOffChanged))
            );
        [Description("Drone takeoff."), Category("Source")]
        public bool TakeOff {
            get { return (bool)this.GetDispatcherValue(TakeOffProperty); }
            set { this.SetDispatcherValue(TakeOffProperty, value); }
        }
        private static void OnTakeOffChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            DroneDevice vm = sender as DroneDevice;
            bool value = (bool)e.NewValue;
            VisualStateManager.GoToState(vm, value ? "TakeOff" : "Station", true);
        }
        #endregion "TakeOff"

        #endregion "Dependency Properties"
    }
}

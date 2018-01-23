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

    [TemplateVisualState(Name = "Normal", GroupName = "ViewStates")]
    [TemplateVisualState(Name = "Selected", GroupName = "ViewStates")]
    public class CameraDevice : BaseVideoDevice {
        public CameraDevice() {
            BaseZIndex = 100;
        }
        static CameraDevice() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CameraDevice), new FrameworkPropertyMetadata(typeof(CameraDevice)));
        }

        #region "Dependency Properties"

        #region "Selected"
        public static readonly DependencyProperty SelectedProperty = DependencyProperty.Register(
            "Selected", typeof(bool), typeof(CameraDevice),
            new FrameworkPropertyMetadata(false,
                new PropertyChangedCallback(OnSelectedChanged))
            );
        [Description("Camera Selected."), Category("Source")]
        public bool Selected {
            get { return (bool)this.GetDispatcherValue(SelectedProperty); }
            set { this.SetDispatcherValue(SelectedProperty, value); }
        }
        private static void OnSelectedChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            CameraDevice vm = sender as CameraDevice;
            bool value = (bool)e.NewValue;
            VisualStateManager.GoToState(vm, value ? "Selected" : "Normal", true);
        }
        #endregion "Selected"

        #endregion "Dependency Properties"
    }

}

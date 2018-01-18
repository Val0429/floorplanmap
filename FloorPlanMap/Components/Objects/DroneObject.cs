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
    [TemplateVisualState(Name = "Station", GroupName = "TakeOffStates")]
    [TemplateVisualState(Name = "Fly", GroupName = "TakeOffStates")]
    public class DroneObject : BaseVideoObject {
        static DroneObject() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DroneObject), new FrameworkPropertyMetadata(typeof(DroneObject)));
        }
    }
}

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

namespace FloorPlanMap.Components.Footprints {
    public class NormalFootprint : Control {
        static NormalFootprint() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NormalFootprint), new FrameworkPropertyMetadata(typeof(NormalFootprint)));
        }
    }
}

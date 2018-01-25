using FloorPlanMap.Components.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
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
    public class NormalFootprint : BaseFootprint {
        static NormalFootprint() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NormalFootprint), new FrameworkPropertyMetadata(typeof(NormalFootprint)));
        }

        #region "Dependency Properties"

        #region "Color"
        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
                "Color", typeof(Color), typeof(NormalFootprint),
                new FrameworkPropertyMetadata((Color)ColorConverter.ConvertFromString("Brown")));
        [Description("Footprint Color."), Category("Source")]
        public Color Color {
            get { return (Color)this.GetDispatcherValue(ColorProperty); }
            set { this.SetDispatcherValue(ColorProperty, value); }
        }
        #endregion "Color"

        #endregion "Dependency Properties"
    }
}

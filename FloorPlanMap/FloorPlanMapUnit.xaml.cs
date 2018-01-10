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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FloorPlanMap
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class FloorPlanMapUnit : UserControl
    {
        public FloorPlanMapUnit() {
            InitializeComponent();
        }

        #region "Dependency Properties"
        static FloorPlanMapUnit() {
            MapSourceProperty = DependencyProperty.Register(
                "MapSource", typeof(string), typeof(FloorPlanMapUnit), null);
        }

        public static readonly DependencyProperty MapSourceProperty;
        [Description("Background map."), Category("Source")]
        public string MapSource {
            get { return (string)GetValue(MapSourceProperty); }
            set { SetValue(MapSourceProperty, value); Console.WriteLine("Value! {0}", value); }
        }

        #endregion "Dependency Properties"

    }
}

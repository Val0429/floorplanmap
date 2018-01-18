using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace FloorPlanMap.Components.Objects {
    public class BaseVideoObject : BaseObject {
        #region "Dependency Properties"

        #region "Degree"
        public static readonly DependencyProperty DegreeProperty = DependencyProperty.Register(
                "Degree", typeof(double), typeof(BaseVideoObject),
                new FrameworkPropertyMetadata(1.5));
        [Description("Camera view degree (wideness)."), Category("Source")]
        public double Degree {
            get { return (double)this.GetDispatcherValue(DegreeProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(DegreeProperty, value, 1500); }
        }
        #endregion "Degree"

        #region "Distance"
        public static readonly DependencyProperty DistanceProperty = DependencyProperty.Register(
                "Distance", typeof(double), typeof(BaseVideoObject),
                new FrameworkPropertyMetadata(3.0));
        [Description("Camera view light distance."), Category("Source")]
        public double Distance {
            get { return (double)this.GetDispatcherValue(DistanceProperty); }
            set { this.SetDispatcherAnimationValue<DoubleAnimation>(DistanceProperty, value, 600); }
        }
        #endregion "Distance"

        #endregion "Dependency Properties"
    }
}

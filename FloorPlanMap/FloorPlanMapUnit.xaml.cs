using FloorPlanMap.Components.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using ObjectType = FloorPlanMap.Components.BaseComponent;
//using ObjectCollection = System.Collections.ObjectModel.ObservableCollection<FloorPlanMap.Components.Objects.CameraObject>;
using ObjectCollection = System.Windows.FreezableCollection<FloorPlanMap.Components.BaseComponent>;
using System.Collections.Specialized;

namespace FloorPlanMap
{
    public partial class FloorPlanMapUnit : UserControl
    {
        public FloorPlanMapUnit() {
            InitializeComponent();

            ObjectCollection collection = new ObjectCollection();
            (collection as INotifyCollectionChanged).CollectionChanged += OnObjectsCollectionChanged;
            SetValue(ObjectsProperty, collection);
        }

        private Point lastMousePos = new Point(0, 0);
        protected override void OnMouseWheel(MouseWheelEventArgs e) {
            Point position = e.GetPosition(this);

            if (e.Delta > 0) ZoomScale *= 1.1;
            if (e.Delta < 0 && ZoomScale > 1) ZoomScale /= 1.1;

            if (lastMousePos.X != position.X || lastMousePos.Y != position.Y) {
                ScaleCenterX += (position.X - ScaleCenterX) / ZoomScale;
                ScaleCenterY += (position.Y - ScaleCenterY) / ZoomScale;
                lastMousePos = position;
            }
        }

        #region "Dependency Properties"

        #region "ZoomScale"
        public static readonly DependencyProperty ZoomScaleProperty = DependencyProperty.Register(
                "ZoomScale", typeof(double), typeof(FloorPlanMapUnit), new PropertyMetadata(1.0));
        [Description("Zoom Scale."), Category("Source")]
        public double ZoomScale {
            get { return (double)GetValue(ZoomScaleProperty); }
            set { SetValue(ZoomScaleProperty, value); }
        }
        #endregion "ZoomScale"

        #region "ScaleCenterX"
        public static readonly DependencyProperty ScaleCenterXProperty = DependencyProperty.Register(
                "ScaleCenterX", typeof(double), typeof(FloorPlanMapUnit), new PropertyMetadata(0.0));
        [Description("Scale Center X."), Category("Source")]
        public double ScaleCenterX {
            get { return (double)GetValue(ScaleCenterXProperty); }
            set { SetValue(ScaleCenterXProperty, value); }
        }
        #endregion "ScaleCenterX"

        #region "ScaleCenterY"
        public static readonly DependencyProperty ScaleCenterYProperty = DependencyProperty.Register(
                "ScaleCenterY", typeof(double), typeof(FloorPlanMapUnit), new PropertyMetadata(0.0));
        [Description("Scale Center Y."), Category("Source")]
        public double ScaleCenterY {
            get { return (double)GetValue(ScaleCenterYProperty); }
            set { SetValue(ScaleCenterYProperty, value); }
        }
        #endregion "ScaleCenterY"

        #region "MainSource"
        public static readonly DependencyProperty MapSourceProperty = DependencyProperty.Register(
                "MapSource", typeof(string), typeof(FloorPlanMapUnit), null);
        [Description("Background map."), Category("Source")]
        public string MapSource {
            get { return (string)GetValue(MapSourceProperty); }
            set { SetValue(MapSourceProperty, value); }
        }
        #endregion "MainSource"

        #region "Objects"
        public static readonly DependencyProperty ObjectsProperty = DependencyProperty.Register(
            "Objects", typeof(ObjectCollection), typeof(FloorPlanMapUnit), null);
        public ObjectCollection Objects {
            get { return (ObjectCollection)GetValue(ObjectsProperty); }
        }
        private void OnObjectsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            /// removed
            var oldItems = e.OldItems;
            if (oldItems != null)
                foreach (var item in oldItems) OnObjectsCollectionRemoved(item as ObjectType);
            /// added
            var newItems = e.NewItems;
            if (newItems != null)
                foreach (var item in newItems) OnObjectsCollectionAdded(item as ObjectType);
        }
        private void OnObjectsCollectionAdded(ObjectType value) {
            Console.WriteLine("Added! {0}", value);
            Main.Children.Add(value);
        }
        private void OnObjectsCollectionRemoved(ObjectType value) {
            Console.WriteLine("Removed! {0}", value);
            Main.Children.Remove(value);
        }
        #endregion "Objects"

        #endregion "Dependency Properties"

    }
}

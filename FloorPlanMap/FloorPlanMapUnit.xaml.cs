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
using ObjectCollection = System.Collections.ObjectModel.ObservableCollection<System.Windows.Controls.Control>;
//using ObjectCollection = System.Windows.FreezableCollection<FloorPlanMap.Components.BaseComponent>;
//using ObjectCollection = System.Windows.FreezableCollection<System.Windows.Controls.Control>;
using System.Collections.Specialized;
using System.Windows.Media.Animation;

namespace FloorPlanMap
{
    public partial class FloorPlanMapUnit : UserControl
    {
        public FloorPlanMapUnit() {
            InitializeComponent();

            ObjectCollection collection = new ObjectCollection();
            //(collection as INotifyCollectionChanged).CollectionChanged += OnObjectsCollectionChanged;
            SetValue(ObjectsProperty, collection);
        }

        #region "Mouse PTZ Function"
        private Point lastMousePos = new Point(0, 0);
        protected override void OnMouseWheel(MouseWheelEventArgs e) {
            Point position = e.GetPosition(this);

            int newZoomLevel = 0;
            if (e.Delta > 0) {
                newZoomLevel = Math.Min(_zoomLevel + 1, MaxZoomLevel);
            } else if (e.Delta < 0) {
                newZoomLevel = Math.Max(_zoomLevel - 1, 1);
            }
            if (newZoomLevel == _zoomLevel) return;
            _zoomLevel = newZoomLevel;
            ZoomScale = Math.Pow(_zoomRatio, _zoomLevel-1);

            if (lastMousePos.X != position.X || lastMousePos.Y != position.Y) {
                ScaleCenterX += (position.X - ScaleCenterX) / ZoomScale;
                ScaleCenterY += (position.Y - ScaleCenterY) / ZoomScale;
                lastMousePos = position;
            }
        }

        private bool dragging = false;
        private Point? dragLastPosition = null;
        protected override void OnMouseDown(MouseButtonEventArgs e) {
            (e.Source as FrameworkElement).CaptureMouse();
            base.OnMouseDown(e);
            dragLastPosition = e.GetPosition(this);
            dragging = true;
        }

        protected override void OnMouseUp(MouseButtonEventArgs e) {
            (e.Source as FrameworkElement).ReleaseMouseCapture();
            base.OnMouseUp(e);
            dragging = false;
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            if (!dragging) return;
            Point pos = e.GetPosition(this);

            double scale = ZoomScale;

            var scx = ScaleCenterX - (pos.X - dragLastPosition.Value.X) / scale;
            scx = Math.Min(Math.Max(scx, 0), Border.ActualWidth);
            var scy = ScaleCenterY - (pos.Y - dragLastPosition.Value.Y) / scale;
            scy = Math.Min(Math.Max(scy, 0), Border.ActualHeight);

            dragLastPosition = pos;

            Animation = false;
            ScaleCenterX = scx;
            ScaleCenterY = scy;
            Animation = true;
        }

        protected override void OnMouseLeave(MouseEventArgs e) {
            base.OnMouseLeave(e);
            dragging = false;
        }
        #endregion "Mouse PTZ Function"

        #region "Dependency Properties"

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

        public ObjectCollection Objects {
            get { return (ObjectCollection)GetValue(ObjectsProperty); }
            set { SetValue(ObjectsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CustomContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectsProperty =
            DependencyProperty.Register("Objects", typeof(ObjectCollection), typeof(FloorPlanMapUnit), new FrameworkPropertyMetadata(
                null, FrameworkPropertyMetadataOptions.AffectsRender
                ));

        //public static readonly DependencyProperty ObjectsProperty = DependencyProperty.Register(
        //    "Objects", typeof(ObjectCollection), typeof(FloorPlanMapUnit), null);
        //public ObjectCollection Objects {
        //    get { return (ObjectCollection)GetValue(ObjectsProperty); }
        //}
        //private void OnObjectsCollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
        //    /// removed
        //    var oldItems = e.OldItems;
        //    if (oldItems != null)
        //        foreach (var item in oldItems) OnObjectsCollectionRemoved(item as ObjectType);
        //    /// added
        //    var newItems = e.NewItems;
        //    if (newItems != null)
        //        foreach (var item in newItems) OnObjectsCollectionAdded(item as ObjectType);
        //}
        //private void OnObjectsCollectionAdded(ObjectType value) {
        //    Console.WriteLine("Added! {0}", value);
        //    Main.Children.Add(value);
        //}
        //private void OnObjectsCollectionRemoved(ObjectType value) {
        //    Console.WriteLine("Removed! {0}", value);
        //    Main.Children.Remove(value);
        //}
        #endregion "Objects"

        #region "MaxZoomLevel"
        private int _zoomLevel = 1;
        private double _zoomRatio = 1.2;
        public static readonly DependencyProperty MaxZoomLevelProperty = DependencyProperty.Register(
                "MaxZoomLevel", typeof(int), typeof(FloorPlanMapUnit), new PropertyMetadata(12));
        [Description("Max Zoom Level."), Category("Source")]
        public int MaxZoomLevel {
            get { return (int)GetValue(MaxZoomLevelProperty); }
            set { SetValue(MaxZoomLevelProperty, value); }
        }
        #endregion "MaxZoomLevel"

        #endregion "Dependency Properties"

        #region "Private Dependency Properties"
        #region "ZoomScale"
        private static readonly DependencyProperty ZoomScaleProperty = DependencyProperty.Register(
                "ZoomScale", typeof(double), typeof(FloorPlanMapUnit), new PropertyMetadata(1.0));
        [Description("Zoom Scale."), Category("Source")]
        private double ZoomScale {
            get { return (double)GetValue(ZoomScaleProperty); }
            set { SetDispatcherAnimationValue<DoubleAnimation>(ZoomScaleProperty, value, 100); }
        }
        #endregion "ZoomScale"

        #region "ScaleCenterX"
        private static readonly DependencyProperty ScaleCenterXProperty = DependencyProperty.Register(
                "ScaleCenterX", typeof(double), typeof(FloorPlanMapUnit), new PropertyMetadata(0.0));
        [Description("Scale Center X."), Category("Source")]
        private double ScaleCenterX {
            get { return (double)GetValue(ScaleCenterXProperty); }
            set { SetDispatcherAnimationValue<DoubleAnimation>(ScaleCenterXProperty, value, 100); }
        }
        #endregion "ScaleCenterX"

        #region "ScaleCenterY"
        private static readonly DependencyProperty ScaleCenterYProperty = DependencyProperty.Register(
                "ScaleCenterY", typeof(double), typeof(FloorPlanMapUnit), new PropertyMetadata(0.0));
        [Description("Scale Center Y."), Category("Source")]
        private double ScaleCenterY {
            get { return (double)GetValue(ScaleCenterYProperty); }
            set { SetDispatcherAnimationValue<DoubleAnimation>(ScaleCenterYProperty, value, 100); }
        }
        #endregion "ScaleCenterY"

        #region "Animation"
        private static readonly DependencyProperty AnimationProperty = DependencyProperty.Register(
                "Animation", typeof(bool), typeof(FloorPlanMapUnit),
                new FrameworkPropertyMetadata(true));
        [Description("Animation on / off."), Category("Source")]
        private bool Animation {
            get { return (bool)GetValue(AnimationProperty); }
            set { SetValue(AnimationProperty, value); }
        }
        #endregion "Animation"

        #endregion "Private Dependency Properties"

        #region "Private Helper"
        protected void SetDispatcherAnimationValue<T>(DependencyProperty dp, object value, double duration) where T : AnimationTimeline {
            var rd = Animation ? duration : 0;
            if (rd != 0) {
                var instance = Activator.CreateInstance(typeof(T), new object[] { value, (Duration)TimeSpan.FromMilliseconds(rd) }) as T;
                this.BeginAnimation(dp, null, HandoffBehavior.Compose);
                this.BeginAnimation(dp, instance, HandoffBehavior.Compose);
            } else {
                this.BeginAnimation(dp, null);
                SetValue(dp, value);
            }
        }
        #endregion "Private Helper"

    }
}

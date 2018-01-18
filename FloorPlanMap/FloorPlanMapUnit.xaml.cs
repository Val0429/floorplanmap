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

using ObjectType = FloorPlanMap.Components.Objects.BaseObject;
//using ObjectCollection = System.Collections.ObjectModel.ObservableCollection<FloorPlanMap.Components.Objects.CameraObject>;
using ObjectCollection = System.Windows.FreezableCollection<FloorPlanMap.Components.Objects.BaseObject>;
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

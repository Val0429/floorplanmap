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

namespace FloorPlanMap.Components.Backgrounds {
    [TemplatePart(Name = "ImageBackground", Type = typeof(Image))]
    public class ImageBackground : Control {

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            Image image = base.GetTemplateChild("ImageBackground") as Image;
            //if (image != null) image.SizeChanged += Image_SizeChanged;
        }

        #region "Static Ctor"
        static ImageBackground() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageBackground), new FrameworkPropertyMetadata(typeof(ImageBackground)));

            //BackgroundResizedEvent = EventManager.RegisterRoutedEvent(
            //    "BackgroundResized", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Size>), typeof(ImageBackground));

            MapSourceProperty = DependencyProperty.Register(
                "MapSource", typeof(string), typeof(ImageBackground), null);
            MapWidthProperty = DependencyProperty.Register(
                "MapWidth", typeof(double), typeof(ImageBackground), null);
            MapHeightProperty = DependencyProperty.Register(
                "MapHeight", typeof(double), typeof(ImageBackground), null);
        }
        #endregion "Static Ctor"

        #region "Routed Events"
        //public static readonly RoutedEvent BackgroundResizedEvent;
        
        //public event RoutedPropertyChangedEventHandler<Size> BackgroundResized {
        //    add { AddHandler(BackgroundResizedEvent, value); }
        //    remove { RemoveHandler(BackgroundResizedEvent, value); }
        //}
        #endregion "Routed Events"

        #region "Dependency Properties"

        public static readonly DependencyProperty MapSourceProperty;
        [Description("Background map."), Category("Source")]
        public string MapSource {
            get { return (string)GetValue(MapSourceProperty); }
            set {
                System.Drawing.Image tmp = System.Drawing.Image.FromFile(value);

                //Size oldSize = new Size(MapWidth, MapHeight);
                //Size newSize = new Size(tmp.Width, tmp.Height);
                //RoutedPropertyChangedEventArgs<Size> args =
                //    new RoutedPropertyChangedEventArgs<Size>(oldSize, newSize);
                //args.RoutedEvent = ImageBackground.BackgroundResizedEvent;
                //this.RaiseEvent(args);

                //MapWidth = newSize.Width;
                //MapHeight = newSize.Height;

                SetValue(MapSourceProperty, System.IO.Path.GetFullPath(value));
            }
        }

        public static readonly DependencyProperty MapWidthProperty;
        public static readonly DependencyProperty MapHeightProperty;
        public double MapWidth {
            get { return (double)GetValue(MapWidthProperty); }
            private set { SetValue(MapWidthProperty, value); }
        }
        public double MapHeight {
            get { return (double)GetValue(MapHeightProperty); }
            private set { SetValue(MapHeightProperty, value); }
        }

        #endregion "Dependency Properties"

    }
}

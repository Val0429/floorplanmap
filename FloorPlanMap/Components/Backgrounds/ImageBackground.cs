using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
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

        public static readonly DependencyProperty MapSourceProperty = DependencyProperty.Register(
                "MapSource", typeof(string), typeof(ImageBackground),
                new FrameworkPropertyMetadata(null,
                new PropertyChangedCallback(OnMapSourceChanged),
                new CoerceValueCallback(OnCoerceMapSource)));
        [Description("Background map."), Category("Source")]
        public string MapSource {
            get { return (string)GetValue(MapSourceProperty); }
            set { SetValue(MapSourceProperty, value); }
        }
        private static object OnCoerceMapSource(DependencyObject sender, object baseValue) {
            if (baseValue == null) return baseValue;
            if ((baseValue as string).IndexOf("http") >= 0) return baseValue;
            string absolutePath = System.IO.Path.GetFullPath(baseValue as string);
            return absolutePath;
        }
        private static void OnMapSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            ImageBackground vm = sender as ImageBackground;
            string value = (string)e.NewValue;
            if (value == null) return;
            try {
                if (value.IndexOf("http") >= 0) {
                    using (WebClient client = new WebClient()) {
                        string tempPath = System.IO.Path.GetTempFileName() + ".png";
                        client.DownloadFile(new Uri(value), tempPath);
                        value = tempPath;
                    }
                }

                System.Drawing.Image tmp = System.Drawing.Image.FromFile(value);
                vm.MapWidth = tmp.Width;
                vm.MapHeight = tmp.Height;
            }
            catch (Exception) {
                vm.MapWidth = 1000;
                vm.MapHeight = 800;
            }
        }

        public static readonly DependencyProperty MapWidthProperty = DependencyProperty.Register(
                "MapWidth", typeof(double), typeof(ImageBackground), null);
        public static readonly DependencyProperty MapHeightProperty = DependencyProperty.Register(
                "MapHeight", typeof(double), typeof(ImageBackground), null);
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

    [ValueConversion(typeof(string), typeof(string))]
    public class FullPathConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null) return value;
            if ((value as string).IndexOf("http") >= 0) return value;
            return System.IO.Path.GetFullPath(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
    }
}

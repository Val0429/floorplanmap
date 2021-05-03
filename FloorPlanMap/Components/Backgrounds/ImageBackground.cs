using Library.Helpers;
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
    class StoreWH {
        public int width { get; set; }
        public int height { get; set; }
    }

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
            if (
                (baseValue as string).IndexOf("http") == 0 ||
                (baseValue as string).IndexOf("pack") == 0
                ) return baseValue;
            string absolutePath = System.IO.Path.GetFullPath(baseValue as string);
            return absolutePath;
        }

        private static Dictionary<string, StoreWH> mapSources = new Dictionary<string, StoreWH>();
        private static void OnMapSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            ImageBackground vm = sender as ImageBackground;
            string value = (string)e.NewValue;
            if (value == null) return;

            if (mapSources.ContainsKey(value)) {
                var result = mapSources[value];
                vm.MapWidth = result.width;
                vm.MapHeight = result.height;
                return;
            }

            try {
                if (value.IndexOf("pack:") == 0) {
                    var tmp = new System.Windows.Media.Imaging.BitmapImage(new Uri(value));
                    mapSources[value] = new StoreWH() {
                        width = (int)tmp.Width,
                        height = (int)tmp.Height
                    };
                    vm.MapWidth = tmp.Width;
                    vm.MapHeight = tmp.Height;

                }
                else {
                    string tempPath = value;
                    if (value.IndexOf("http") == 0) {
                        using (WebClient client = new WebClient()) {
                            tempPath = System.IO.Path.GetTempFileName() + ".png";
                            //client.DownloadFile(new Uri(value), tempPath);
                            client.DownloadFileTaskAsync(new Uri(value), tempPath)
                                .ContinueWith(ex => {
                                    sender.Dispatcher.InvokeAsyncSafe(() =>
                                    {
                                        vm.MapWidth = 1000;
                                        vm.MapHeight = 800;
                                    });
                                }, TaskContinuationOptions.OnlyOnFaulted)
                                .ContinueWith(ex => {
                                    var tmp = System.Drawing.Image.FromFile(tempPath);

                                    sender.Dispatcher.InvokeAsyncSafe(() => {
                                        mapSources[value] = new StoreWH()
                                        {
                                            width = tmp.Width,
                                            height = tmp.Height
                                        };
                                        vm.MapWidth = tmp.Width;
                                        vm.MapHeight = tmp.Height;
                                    });
                                });
                        }
                    }
                }
            }
            catch (Exception ex) {
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
            if (
                (value as string).IndexOf("http") == 0 ||
                (value as string).IndexOf("pack") == 0
                ) return value;
            return System.IO.Path.GetFullPath(value as string);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }
    }
}

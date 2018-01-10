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
            if (image != null) image.SizeChanged += Image_SizeChanged;
        }

        private void Image_SizeChanged(object sender, SizeChangedEventArgs e) {
            Image image = sender as Image;
            MapWidth = image.ActualWidth;
            MapHeight = image.ActualHeight;
        }

        #region "Dependency Properties"

        static ImageBackground() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageBackground), new FrameworkPropertyMetadata(typeof(ImageBackground)));

            MapSourceProperty = DependencyProperty.Register(
                "MapSource", typeof(string), typeof(ImageBackground), null);
            MapWidthProperty = DependencyProperty.Register(
                "MapWidth", typeof(double), typeof(ImageBackground), null);
            MapHeightProperty = DependencyProperty.Register(
                "MapHeight", typeof(double), typeof(ImageBackground), null);
        }

        public static readonly DependencyProperty MapSourceProperty;
        [Description("Background map."), Category("Source")]
        public string MapSource {
            get { return (string)GetValue(MapSourceProperty); }
            set { SetValue(MapSourceProperty, value); }
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

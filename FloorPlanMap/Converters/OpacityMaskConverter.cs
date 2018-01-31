using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace FloorPlanMap.Converters {

    [ValueConversion(typeof(double), typeof(Color))]
    public class OpacityMaskConverter : IValueConverter {
        //object IMultiValueConverter.Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
        //    /// 0: Color color
        //    /// 1: double opacity
        //    Color color = (Color)values[0];
        //    double opacity = (double)values[1];

        //    byte ob = (byte)(opacity * 255);
        //    return Color.FromArgb(ob, 0, 0, 0);
        //}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            double opacity = (double)value;
            byte ob = (byte)(opacity * 255);
            return Color.FromArgb(ob, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}

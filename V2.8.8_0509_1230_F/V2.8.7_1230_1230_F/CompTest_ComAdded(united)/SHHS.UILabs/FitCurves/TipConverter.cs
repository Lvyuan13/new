using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace SHHS.UILabs.FitCurves
{
    public class TipConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            string tip = "";

            if (values[0] != null)
                tip = values[0].ToString();
            tip = tip + "[" + values[1] + "," + values[2] + "]";

            return tip;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            string[] splitValues = ((string)value).Split(' ');
            return splitValues;
        }
    }
}

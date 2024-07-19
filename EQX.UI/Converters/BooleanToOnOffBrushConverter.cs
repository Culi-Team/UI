using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EQX.UI.Converters
{
    internal class InOutStatusToOnOffBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] is bool bValue && values[1] is string strName)
            {
                if (strName.ToUpper().StartsWith("SPARE")) return Brushes.DarkGray;
                return bValue ? Brushes.Lime : Brushes.Tomato;
            }
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    internal class BooleanToOnOffBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool bValue)
            {
                return bValue ? Brushes.Lime : Brushes.Tomato;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EQX.UI.Converters
{
    internal class BooleanToAlarmBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool onOff = (bool)value;

            return onOff ? Brushes.Red : Brushes.Silver;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EQX.UI.Converters
{
    internal class BooleanToOnOffBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is bool bValue)
            {
                return bValue ? Brushes.Green : Brushes.Red;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

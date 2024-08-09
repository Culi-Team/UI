using EQX.Core.Sequence;
using System.Globalization;
using System.Windows.Data;

namespace UTGAutoLoadUnload.Converters
{
    public class ProcessModeToIsEnableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is EProcessMode processMode)
            {
                return processMode == EProcessMode.None ||
                processMode == EProcessMode.Alarm ||
                processMode == EProcessMode.Warning ||
                processMode == EProcessMode.Stop;
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

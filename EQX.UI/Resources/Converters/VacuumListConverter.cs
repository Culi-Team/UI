using EQX.Core.InOut;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace UTGAutoLoadUnload.Converters
{
    public class VacuumListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            List<IDOutput> vacuums = new List<IDOutput>();
            if (value is ObservableCollection<IDOutput> vacuumsList == false) return Binding.DoNothing;

            PropertyInfo[] properties = value.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(vacuumsList, null) == null) continue;
                vacuums.Add((IDOutput)property.GetValue(vacuumsList, null));
            }
            return vacuums;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

    }
}

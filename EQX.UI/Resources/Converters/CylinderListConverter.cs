using EQX.Core.InOut;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;
using UTGAutoLoadUnload.Defines.Devices;

namespace UTGAutoLoadUnload.Converters
{
    public class CylinderListConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<ICylinder> cylinders = new ObservableCollection<ICylinder>();
            if (value is CylinderList cylinderList == false) return Binding.DoNothing;

            PropertyInfo[] properties = value.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if(property.GetValue(cylinderList, null) == null) continue;
                cylinders.Add((ICylinder)property.GetValue(cylinderList, null));
            }

            return cylinders;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}

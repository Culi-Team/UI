using EQX.Core.InOut;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace EQX.UI.Converters
{
    public class CylinderTypeToButtonNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ECylinderType cylinderType && parameter is string strStatus)
            {
                bool status1 = strStatus == "true";

                switch (cylinderType)
                {
                    case ECylinderType.ForwardBackward:
                    case ECylinderType.ForwardBackwardReverse:
                        return status1 ? "Forward" : "Backward";
                    case ECylinderType.UpDown:
                    case ECylinderType.UpDownReverse:
                        return status1 ? "Up" : "Down";
                    case ECylinderType.RightLeft:
                    case ECylinderType.RightLeftReverse:
                        return status1 ? "Right" : "Left";
                    case ECylinderType.GripUngrip:
                        return status1 ? "Grip" : "Ungrip";
                    case ECylinderType.GripUngripReverse:
                        return status1 ? "Ungrip" : "Grip";
                    case ECylinderType.AlignUnalign:
                        return status1 ? "Align" : "Unalign";
                    case ECylinderType.AlignUnalignReverse:
                        return status1 ? "Unalign" : "Align";
                    case ECylinderType.LockUnlock:
                        return status1 ? "Lock" : "Unlock";
                    case ECylinderType.LockUnlockReverse:
                        return status1 ? "Unlock" : "Lock";
                    default:
                        return Binding.DoNothing;
                }
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }

    public class AddSpacesToSentenceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string strValue)
            {
                return AddSpacesToSentence(strValue);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }

        private string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}

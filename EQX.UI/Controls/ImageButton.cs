using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EQX.UI.Controls
{
    public class ImageButton : Button
    {
        public ImageSource Image
        {
            get { return (ImageSource)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Image.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageProperty =
            DependencyProperty.Register("Image", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));


        public ImageSource DisabledImage
        {
            get { return (ImageSource)GetValue(DisabledImageProperty); }
            set { SetValue(DisabledImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisabledImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisabledImageProperty =
            DependencyProperty.Register("DisabledImage", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));
    }
}

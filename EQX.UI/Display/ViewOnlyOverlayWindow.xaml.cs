using System.Windows;

namespace EQX.UI.Display
{
    /// <summary>
    /// Interaction logic for ViewOnlyOverlayWindow.xaml
    /// </summary>
    public partial class ViewOnlyOverlayWindow : Window
    {
        public ViewOnlyOverlayWindow()
        {
            InitializeComponent();
            MirrorBrush.Visual = Application.Current.MainWindow;
        }

        private void Eat(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}

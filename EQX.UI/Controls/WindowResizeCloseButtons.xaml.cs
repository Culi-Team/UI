using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for WindowResizeCloseButtons.xaml
    /// </summary>
    public partial class WindowResizeCloseButtons : UserControl
    {
        public WindowState WindowState
        {
            get
            {
                if (Window.GetWindow(this) == null) return WindowState.Normal;

                return Window.GetWindow(this).WindowState;
            }
            set
            {
                if (Window.GetWindow(this) == null) return;

                Window.GetWindow(this).WindowState = value;
            }
        }

        public WindowResizeCloseButtons()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void MinimizeButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButtonClick(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Maximized ?
                          WindowState.Normal
                        : WindowState.Maximized;
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}

using EQX.Core.Common;
using System.Windows;
using System.Windows.Input;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for AlertNotifyView.xaml
    /// </summary>
    public partial class AlertNotifyView : Window
    {
        public AlertModel AlertModel { get; set; }
        public bool IsWarning { get; set; }

        public AlertNotifyView()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        
        public static bool? ShowDialog(AlertModel alarmModel, bool isWarning = false)
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                bool isShown = Application.Current.Windows.OfType<AlertNotifyView>().Any();
                if (isShown) Application.Current.Windows.OfType<AlertNotifyView>().First().Close();

                isShown = Application.Current.Windows.OfType<MessageBoxEx>().Any();
                if (isShown) Application.Current.Windows.OfType<MessageBoxEx>().First().Close();

                AlertNotifyView messageBoxEx = new AlertNotifyView()
                {
                    IsWarning = isWarning,
                    AlertModel = alarmModel
                };

                messageBoxEx.ShowDialog();
                return messageBoxEx.DialogResult;
            });
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Interop.ComponentDispatcher.IsThreadModal)
            {
                DialogResult = false;
            }
            else
            {
                Close();
            }
        }
    }
}

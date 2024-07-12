using EQX.Core.Common;
using System.Windows;
using System.Windows.Input;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for AlarmNotifyView.xaml
    /// </summary>
    public partial class AlarmNotifyView : Window
    {
        public AlarmModel AlarmModel { get; set; }

        public AlarmNotifyView()
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
        
        public static bool? ShowDialog(AlarmModel alarmModel)
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                bool isShown = Application.Current.Windows.OfType<AlarmNotifyView>().Any();
                if (isShown) Application.Current.Windows.OfType<AlarmNotifyView>().First().Close();

                AlarmNotifyView messageBoxEx = new AlarmNotifyView()
                {
                    AlarmModel = alarmModel
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

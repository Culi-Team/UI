using System.Windows;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for MessageBoxEx.xaml
    /// </summary>
    public partial class MessageBoxEx : Window
    {
        public MessageBoxEx()
        {
            InitializeComponent();
        }

        public static void Show(string message, string caption = "Confirm")
        {
            // TODO: Check if any MessageBoxEx alive

            MessageBoxEx messageBoxEx = new MessageBoxEx();
            ((MessageBoxExViewModel)messageBoxEx.DataContext).Show(message, caption);
            messageBoxEx.Show();
        }

        public static bool? ShowDialog(string message, string caption = "Confirm")
        {
            // TODO: Check if any MessageBoxEx alive

            if (Application.Current.Dispatcher.CheckAccess())
            {
                // The current thread is the UI thread
                MessageBoxEx messageBoxEx = new MessageBoxEx();
                ((MessageBoxExViewModel)messageBoxEx.DataContext).ShowDialog(message, caption);
                messageBoxEx.ShowDialog();
                return messageBoxEx.DialogResult;
            }
            else
            {
                // The current thread is not the UI thread =>  call to the UI thread
                return Application.Current.Dispatcher.Invoke(() =>
                {
                    MessageBoxEx messageBoxEx = new MessageBoxEx();
                    ((MessageBoxExViewModel)messageBoxEx.DataContext).ShowDialog(message, caption);
                    messageBoxEx.ShowDialog();
                    return messageBoxEx.DialogResult;
                });
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

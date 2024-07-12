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
            Application.Current.Dispatcher.Invoke(() =>
            {
                bool isShown = Application.Current.Windows.OfType<MessageBoxEx>().Any();
                if (isShown) Application.Current.Windows.OfType<MessageBoxEx>().First().Close();

                MessageBoxEx messageBoxEx = new MessageBoxEx();
                ((MessageBoxExViewModel)messageBoxEx.DataContext).Show(message, caption);
                messageBoxEx.Show();
            });
        }

        public static bool? ShowDialog(string message, string caption = "Confirm")
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                bool isShown = Application.Current.Windows.OfType<MessageBoxEx>().Any();
                if (isShown) Application.Current.Windows.OfType<MessageBoxEx>().First().Close();

                MessageBoxEx messageBoxEx = new MessageBoxEx();
                ((MessageBoxExViewModel)messageBoxEx.DataContext).ShowDialog(message, caption);
                messageBoxEx.ShowDialog();
                return messageBoxEx.DialogResult;
            });
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Interop.ComponentDispatcher.IsThreadModal)
            {
                DialogResult = true;
            }
            else
            {
                Close();
            }
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

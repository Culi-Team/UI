using CommunityToolkit.Mvvm.ComponentModel;
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
using System.Windows.Shapes;

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

            MessageBoxEx messageBoxEx = new MessageBoxEx();
            ((MessageBoxExViewModel)messageBoxEx.DataContext).ShowDialog(message, caption);
            messageBoxEx.ShowDialog();

            return messageBoxEx.DialogResult;
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

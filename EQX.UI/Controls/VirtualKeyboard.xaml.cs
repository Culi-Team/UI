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
using WindowsInput;
using WindowsInput.Native;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for VirtualKeyboard.xaml
    /// </summary>
    public partial class VirtualKeyboard : Window
    {
        public string InputText { get; set; }

        public VirtualKeyboard()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn == false) return;

            passwordBox.Password += btn.Content.ToString();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            InputText = passwordBox.Password;
        }

        private void BackspaceButton_Click(object sender, RoutedEventArgs e)
        {
            if(passwordBox.Password.Length <= 0 ) return;
            passwordBox.Password = passwordBox.Password[..^1];
        }
    }
}

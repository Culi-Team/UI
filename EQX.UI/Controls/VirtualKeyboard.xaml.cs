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
    /// Interaction logic for VirtualKeyboard.xaml
    /// </summary>
    public partial class VirtualKeyboard : Window
    {
        public string InputText { get; set; } = string.Empty;

        public VirtualKeyboard()
        {
            InitializeComponent();
            // Set focus to window instead of any button
            Loaded += (s, e) => Focus();
            
            // Add touch event handler to all buttons
            Loaded += (s, args) =>
            {
                foreach (var button in FindVisualChildren<Button>(this))
                {
                    button.PreviewTouchDown += Button_PreviewTouchDown;
                }
            };
        }
        
        private void Button_PreviewTouchDown(object sender, TouchEventArgs e)
        {
            // Immediately trigger the click event
            if (sender is Button button)
            {
                var clickArgs = new RoutedEventArgs(Button.ClickEvent, button);
                button.RaiseEvent(clickArgs);
                e.Handled = true;
            }
        }
        
        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T t)
                    {
                        yield return t;
                    }
                    
                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
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

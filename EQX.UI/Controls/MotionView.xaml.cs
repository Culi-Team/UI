using EQX.Motion;
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
    /// Interaction logic for MotionView.xaml
    /// </summary>
    public partial class MotionView : UserControl
    {
        public MotionView()
        {
            InitializeComponent();
            cbBoxStepInc.ItemsSource = new List<int> { 1, 2, 5, 10 };
            IncMode.IsChecked = true;
        }

        private void MoveDec_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(JogMode.IsChecked == true) 
            {
                (this.DataContext as MotionBase).MoveJog(2000, false);
            }
            else
            {
                (this.DataContext as MotionBase).MoveInc(-(double)cbBoxStepInc.SelectedItem, 2000);
            }
        }

        private void MoveDec_ButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (JogMode.IsChecked == true)
            {
                (this.DataContext as MotionBase).Stop();
            }
        }

        private void MoveInc_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (JogMode.IsChecked == true)
            {
                (this.DataContext as MotionBase).MoveJog(2000, true);
            }
            else
            {
                (this.DataContext as MotionBase).MoveInc((double)cbBoxStepInc.SelectedItem, 2000);
            }
        }

        private void MoveInc_ButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (JogMode.IsChecked == true)
            {
                (this.DataContext as MotionBase).Stop();
            }
        }

        private void ServoOnButton_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MotionBase).MotionOn();
        }

        private void OriginButton_Click(object sender, RoutedEventArgs e)
        {
            (this.DataContext as MotionBase).SearchOrigin();
        }
    }
}

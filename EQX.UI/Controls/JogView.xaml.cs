using EQX.Core.Motion;
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
    /// Interaction logic for JogView.xaml
    /// </summary>
    public partial class JogView : UserControl
    {
        public JogView()
        {
            InitializeComponent();
        }

        private List<string> jogSpeedList = new List<string>
        {
            "SuperSlow",
            "Slow",
            "Medium",
            "High",
        };

        private double jogSpeedRate = 5;

        private List<double> jogSpeedRates = new List<double>
        {
            .03,
            .10,
            .20,
            .40,
        };

        private List<double> absDistanceList = new List<double>
        {
            0.001,
            0.010,
            0.1,
            1,
            10,
        };

        private void MoveDec_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsDataContextValid() == false) return;

            if (JogMode.IsChecked == true)
            {
                ((IMotion)DataContext).MoveJog(
                    ((IMotion)DataContext).Parameter.MaxVelocity * jogSpeedRate,
                    false);
            }
            else
            {
                // Move INC by 10% max speed
                ((IMotion)DataContext).MoveInc((double)cbBoxStepInc.SelectedItem * -1,
                   ((IMotion)DataContext).Parameter.MaxVelocity * .10);
            }
        }

        private void MoveButton_ButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsDataContextValid() == false) return;

            if (JogMode.IsChecked == true)
            {
                ((IMotion)DataContext).Stop();
            }
        }

        private void MoveInc_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsDataContextValid() == false) return;

            if (JogMode.IsChecked == true)
            {
                ((IMotion)DataContext).MoveJog(
                    ((IMotion)DataContext).Parameter.MaxVelocity * jogSpeedRate,
                    true);
            }
            else
            {
                // Move INC by 10% max speed
                ((IMotion)DataContext).MoveInc((double)cbBoxStepInc.SelectedItem,
                    ((IMotion)DataContext).Parameter.MaxVelocity * .10);
            }
        }

        private bool IsDataContextValid()
        {
            if (DataContext == null) return false;
            if (DataContext.GetType().GetInterfaces().Contains(typeof(IMotion)) == false) return false;

            return true;
        }

        private void JogAbsMode_Checked(object sender, RoutedEventArgs e)
        {
            cbBoxStepInc.ItemsSource = JogMode.IsChecked == true ? jogSpeedList : absDistanceList;
            cbBoxStepInc.SelectedIndex = 0;
        }

        private void cbBoxStepInc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JogMode.IsChecked == false) return;
            if (cbBoxStepInc.SelectedIndex < 0) cbBoxStepInc.SelectedIndex = 0;

            jogSpeedRate = jogSpeedRates[cbBoxStepInc.SelectedIndex];
        }
    }
}
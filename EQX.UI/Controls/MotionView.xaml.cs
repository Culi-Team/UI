using EQX.Core.Motion;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for MotionView.xaml
    /// </summary>
    public partial class MotionView : UserControl
    {
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
            .005,
            .02,
            .10,
            .20,
        };

        private List<double> absDistanceList = new List<double>
        {
            0.001,
            0.010,
            0.1,
            1,
            10,
        };

        public MotionView()
        {
            InitializeComponent();
            cbBoxStepInc.ItemsSource = jogSpeedList;
        }

        private void MoveDec_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsDataContextValid() == false) return;

            if (JogMode.IsChecked == true)
            {
                ((IMotion)DataContext).MoveJog(
                    ((IMotion)DataContext).Parameter.MaxVelocity * jogSpeedRates[cbBoxStepInc.SelectedIndex],
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
                    ((IMotion)DataContext).Parameter.MaxVelocity * jogSpeedRates[cbBoxStepInc.SelectedIndex],
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

        private void ButtonServoOn_Click(object sender, RoutedEventArgs e)
        {
            if (IsDataContextValid() == false) return;

            if (((IMotion)DataContext).Status.IsMotionOn)
            {
                ((IMotion)DataContext).MotionOff();
            }
            else
            {
                ((IMotion)DataContext).MotionOn();
            }
        }

        private void ButtonOrigin_Click(object sender, RoutedEventArgs e)
        {
            if (IsDataContextValid() == false) return;

            ((IMotion)DataContext).SearchOrigin();
        }
        private void ButtonResetAlarm_Click(object sender, RoutedEventArgs e)
        {
            if (IsDataContextValid() == false) return;

            ((IMotion)DataContext).AlarmReset();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (IsDataContextValid() == false) return;
            if (((IMotion)DataContext).IsConnected)
            {
                ((IMotion)DataContext).Disconnect();
            }
            else
            {
                ((IMotion)DataContext).Connect();
            }
        }
    }
}

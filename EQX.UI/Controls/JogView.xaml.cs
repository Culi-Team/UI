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

        private bool JogMode = false;

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

        private void MoveDec_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsDataContextValid() == false) return;

            if (JogMode == true)
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

            if (JogMode == true)
            {
                ((IMotion)DataContext).Stop();
            }
        }

        private void MoveInc_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsDataContextValid() == false) return;

            if (JogMode == true)
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

        private void cbBoxStepInc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (JogMode == false) return;
            if (cbBoxStepInc.SelectedIndex < 0) cbBoxStepInc.SelectedIndex = 0;

            jogSpeedRate = jogSpeedRates[cbBoxStepInc.SelectedIndex];
        }

        private void btnJog_Click(object sender, RoutedEventArgs e)
        {
            JogMode = true;
            cbBoxStepInc.ItemsSource = jogSpeedList;
            cbBoxStepInc.SelectedIndex = 0;

            jogSpeedRate = jogSpeedRates[cbBoxStepInc.SelectedIndex];

            btnJog.Background = new SolidColorBrush(Colors.Lime);
            btnInc.Background = new SolidColorBrush(Colors.Tomato);
            btnJog.Opacity = 1;
            btnInc.Opacity = 0.7;
        }

        private void btnInc_Click(object sender, RoutedEventArgs e)
        {
            JogMode = false;
            cbBoxStepInc.ItemsSource = absDistanceList;
            cbBoxStepInc.SelectedIndex = 0;

            jogSpeedRate = absDistanceList[cbBoxStepInc.SelectedIndex];

            btnInc.Background = new SolidColorBrush(Colors.Lime);
            btnJog.Background = new SolidColorBrush(Colors.Tomato);
            btnJog.Opacity = 0.7;
            btnInc.Opacity = 1;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            cbBoxStepInc.ItemsSource = JogMode == true ? jogSpeedList : absDistanceList;
            cbBoxStepInc.SelectedIndex = 0;

            btnInc.Background = new SolidColorBrush(Colors.Lime);
            btnJog.Background = new SolidColorBrush(Colors.Tomato);
            btnJog.Opacity = 0.7;
            btnInc.Opacity = 1;
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
    }
}
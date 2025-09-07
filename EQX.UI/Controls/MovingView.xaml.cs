using EQX.Core.Motion;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for MovingView.xaml
    /// </summary>
    public partial class MovingView : UserControl
    {
        private List<string> jogSpeedList = new List<string>
        {
            (string)Application.Current.Resources["str_SuperSlow"],
            (string)Application.Current.Resources["str_Slow"],
            (string)Application.Current.Resources["str_Medium"],
            (string)Application.Current.Resources["str_High"],
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

        public MovingView()
        {
            InitializeComponent();
            cbBoxStepInc.ItemsSource = jogSpeedList;
        }

        public IMotion Motion
        {
            get { return (IMotion)GetValue(MotionProperty); }
            set { SetValue(MotionProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Motion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MotionProperty =
            DependencyProperty.Register("Motion", typeof(IMotion), typeof(MotionView), new PropertyMetadata(null));

        public bool Interlock
        {
            get { return (bool)GetValue(InterlockProperty); }
            set { SetValue(InterlockProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Interlock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InterlockProperty =
            DependencyProperty.Register("Interlock", typeof(bool), typeof(MotionView), new PropertyMetadata(true));


        private void MoveDec_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsValid() == false) return;
            if (Interlock == false)
            {
                MessageBoxEx.ShowDialog("Interlock Check Error");
                return;
            }

            if (JogMode.IsChecked == true)
            {
                Motion.MoveJog(
                    Motion.Parameter.MaxVelocity * jogSpeedRates[cbBoxStepInc.SelectedIndex],
                    false);
            }
            else
            {
                // Move INC by 10% max speed
                Motion.MoveInc((double)cbBoxStepInc.SelectedItem * -1,
                   Motion.Parameter.MaxVelocity * .10);
            }
        }

        private void MoveButton_ButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (IsValid() == false) return;

            if (JogMode.IsChecked == true)
            {
                Motion.Stop();
            }
        }

        private void MoveInc_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsValid() == false) return;

            if (Interlock == false)
            {
                MessageBoxEx.ShowDialog("Interlock Check Error");
                return;
            }

            if (JogMode.IsChecked == true)
            {
                Motion.MoveJog(
                    Motion.Parameter.MaxVelocity * jogSpeedRates[cbBoxStepInc.SelectedIndex],
                    true);
            }
            else
            {
                // Move INC by 10% max speed
                Motion.MoveInc((double)cbBoxStepInc.SelectedItem,
                    Motion.Parameter.MaxVelocity * .10);
            }
        }

        private bool IsValid()
        {
            if (Motion == null) return false;

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
            if (IsValid() == false) return;

            if (Motion.Status.IsMotionOn)
            {
                Motion.MotionOff();
            }
            else
            {
                Motion.MotionOn();
            }
        }

        private void ButtonOrigin_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid() == false) return;

            Motion.SearchOrigin();
        }
        private void ButtonResetAlarm_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid() == false) return;

            Motion.AlarmReset();
        }

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid() == false) return;
            if (Motion.IsConnected)
            {
                Motion.Disconnect();
            }
            else
            {
                Motion.Connect();
            }
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EQX.Core.Interlock;
using EQX.Core.Motion;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for MotionView.xaml
    /// </summary>
    public partial class MotionView : UserControl
    {
        public string? OriginInterlockKey
        {
            get => (string?)GetValue(OriginInterlockKeyProperty);
            set => SetValue(OriginInterlockKeyProperty, value);
        }

        public static readonly DependencyProperty OriginInterlockKeyProperty =
            DependencyProperty.Register(
                nameof(OriginInterlockKey),
                typeof(string),
                typeof(MotionView),
                new PropertyMetadata(null));

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

        private string? _pendingOriginInterlockKey;

        public MotionView()
        {
            InitializeComponent();
            cbBoxStepInc.ItemsSource = jogSpeedList;
            DataContextChanged += MotionView_DataContextChanged;
            Loaded += MotionView_Loaded;
            Unloaded += MotionView_Unloaded;
        }

        private void MotionView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateOriginInterlockKey(DataContext as IMotion);
        }

        private void MotionView_Loaded(object sender, RoutedEventArgs e)
        {
            InterlockService.Default.InterlockChanged += OnInterlockChanged;
            InterlockService.Default.Reevaluate();
        }

        private void MotionView_Unloaded(object sender, RoutedEventArgs e)
        {
            InterlockService.Default.InterlockChanged -= OnInterlockChanged;
        }

        private void OnInterlockChanged(string key, bool satisfied)
        {
            if (string.Equals(key, _pendingOriginInterlockKey, StringComparison.Ordinal) == false)
            {
                return;
            }

            if (Dispatcher.CheckAccess() == false)
            {
                _ = Dispatcher.InvokeAsync(() => OnInterlockChanged(key, satisfied));
                return;
            }

            OriginInterlockKey = _pendingOriginInterlockKey;
        }

        private void UpdateOriginInterlockKey(IMotion? motion)
        {
            if (motion == null)
            {
                _pendingOriginInterlockKey = null;
                OriginInterlockKey = string.Empty;
                return;
            }

            _pendingOriginInterlockKey = $"Motion.{motion.Name}.Origin";
            OriginInterlockKey = string.Empty;
            InterlockService.Default.Reevaluate();
        }
        private void MoveDec_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsValid() == false) return;

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
            if (IsValid() == false) return;

            if (JogMode.IsChecked == true)
            {
                ((IMotion)DataContext).Stop();
            }
        }

        private void MoveInc_ButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (IsValid() == false) return;

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

        private bool IsValid()
        {
            if (DataContext is  IMotion motion == false) return false;

            return true;
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
            if (IsValid() == false) return;

            ((IMotion)DataContext).SearchOrigin();
        }
        private void ButtonResetAlarm_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid() == false) return;

            ((IMotion)DataContext).AlarmReset();
        }
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid() == false) return;

            ((IMotion)DataContext).Stop();
        }
        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid() == false) return;
            if (((IMotion)DataContext).IsConnected)
            {
                ((IMotion)DataContext).Disconnect();
            }
            else
            {
                ((IMotion)DataContext).Connect();
            }
        }

        private void JogMode_Click(object sender, RoutedEventArgs e)
        {
            cbBoxStepInc.ItemsSource = JogMode.IsChecked == true ? jogSpeedList : absDistanceList;
            cbBoxStepInc.SelectedIndex = 0;
        }
    }
}

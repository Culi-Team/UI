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
using System.Windows.Threading;
using EQX.InOut;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for VacuumView.xaml
    /// </summary>
    public partial class VacuumView : UserControl
    {
        public VacuumView()
        {
            InitializeComponent();
            _reachTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _reachTimer.Tick += ReachTimer_Tick;
            Loaded += (_, _) => UpdateTimerState();
            Unloaded += (_, _) => _reachTimer.Stop();
            DataContextChanged += VacuumView_DataContextChanged;
        }

        public VacuumBase? Vacuum
        {
            get { return (VacuumBase?)GetValue(VacuumProperty); }
            set { SetValue(VacuumProperty, value); }
        }

        public static readonly DependencyProperty VacuumProperty =
            DependencyProperty.Register("Vacuum", typeof(VacuumBase), typeof(VacuumView), new PropertyMetadata(null));

        public bool UseReachTimer
        {
            get { return (bool)GetValue(UseReachTimerProperty); }
            set { SetValue(UseReachTimerProperty, value); }
        }

        public static readonly DependencyProperty UseReachTimerProperty =
            DependencyProperty.Register(
                nameof(UseReachTimer),
                typeof(bool),
                typeof(VacuumView),
                new PropertyMetadata(false, UseReachTimerChanged));
        private void VacuumButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is VacuumBase vacuum)
            {
                vacuum.SuctionOn();
            }
        }

        private void BlowButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is VacuumBase vacuum)
            {
                if (vacuum.HasBlow)
                {
                    vacuum.BlowOn();
                }
                else
                {
                    vacuum.Off();
                }
            }
        }

        private void ReachTimer_Tick(object? sender, EventArgs e)
        {
            _currentVacuum?.UpdateDetectDuration();
            UpdateTimerState();
        }

        private void VacuumView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue is VacuumBase oldVacuum)
            {
                oldVacuum.StateChanged -= Vacuum_StateChanged;
            }

            _currentVacuum = e.NewValue as VacuumBase;

            if (_currentVacuum != null)
            {
                _currentVacuum.StateChanged += Vacuum_StateChanged;
            }

            UpdateTimerState();
        }

        private static void UseReachTimerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is VacuumView view)
            {
                view.UpdateTimerState();
            }
        }

        private void Vacuum_StateChanged(object? sender, EventArgs e)
        {
            if (Dispatcher.CheckAccess())
            {
                UpdateTimerState();
            }
            else
            {
                Dispatcher.BeginInvoke(DispatcherPriority.DataBind, new Action(UpdateTimerState));
            }
        }

        private void UpdateTimerState()
        {
            if (UseReachTimer && _currentVacuum != null && _currentVacuum.HasVacuumDetect && _currentVacuum.IsDetectDurationActive)
            {
                if (!_reachTimer.IsEnabled)
                {
                    _reachTimer.Start();
                }
            }
            else
            {
                if (_reachTimer.IsEnabled)
                {
                    _reachTimer.Stop();
                }
            }
        }

        private VacuumBase? _currentVacuum;
        private readonly DispatcherTimer _reachTimer;
    }
}

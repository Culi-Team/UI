using EQX.Core.InOut;
using EQX.InOut;
using System.Windows;
using System.Windows.Controls;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for CylinderView.xaml
    /// </summary>
    public partial class CylinderView : UserControl
    {
        public CylinderView()
        {
            InitializeComponent();
        }
        public ICylinder Cylinder
        {
            get { return (ICylinder)GetValue(CylinderProperty); }
            set { SetValue(CylinderProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Motion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CylinderProperty =
            DependencyProperty.Register("Cylinder", typeof(ICylinder), typeof(CylinderView), new PropertyMetadata(null));

        public bool Interlock
        {
            get { return (bool)GetValue(InterlockProperty); }
            set { SetValue(InterlockProperty, value); }
        }
        // Using a DependencyProperty as the backing store for Interlock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InterlockProperty =
            DependencyProperty.Register("Interlock", typeof(bool), typeof(CylinderView), new PropertyMetadata(true));
        private async void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CylinderBase cylinder == false)
            {
                return;
            }

            if (!IsInterlockSatisfied(cylinder))
            {
                return;
            }

            var timeout = TimeSpan.FromSeconds(cylinder.ForwardTimeoutSeconds);
            if (IsReverseCylinder(cylinder))
            {
                await cylinder.MoveBackwardWithTimeoutAsync(timeout);
                return;
            }

            await cylinder.MoveForwardWithTimeoutAsync(timeout);
        }

        private async void BackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CylinderBase cylinder == false)
            {
                return;
            }

            if (!IsInterlockSatisfied(cylinder))
            {
                return;
            }

            var timeout = TimeSpan.FromSeconds(cylinder.BackwardTimeoutSeconds);
            if (IsReverseCylinder(cylinder))
            {
                await cylinder.MoveForwardWithTimeoutAsync(timeout);
                return;
            }

            await cylinder.MoveBackwardWithTimeoutAsync(timeout);
        }

        private bool IsInterlockSatisfied(CylinderBase cylinder)
        {
            if (!Interlock || !cylinder.IsInterlockSatisfiedState)
            {
                MessageBoxEx.ShowDialog("Interlock Check Error");
                return false;
            }

            return true;
        }

        private static bool IsReverseCylinder(CylinderBase cylinder)
        {
            return cylinder.CylinderType == Core.InOut.ECylinderType.ForwardBackwardReverse ||
                   cylinder.CylinderType == Core.InOut.ECylinderType.UpDownReverse ||
                   cylinder.CylinderType == Core.InOut.ECylinderType.RightLeftReverse ||
                   cylinder.CylinderType == Core.InOut.ECylinderType.LockUnlockReverse ||
                   cylinder.CylinderType == Core.InOut.ECylinderType.GripUngripReverse ||
                   cylinder.CylinderType == Core.InOut.ECylinderType.AlignUnalignReverse ||
                   cylinder.CylinderType == Core.InOut.ECylinderType.FlipUnflipReverse ||
                   cylinder.CylinderType == Core.InOut.ECylinderType.ClampUnclampReverse;
        }
    }
}

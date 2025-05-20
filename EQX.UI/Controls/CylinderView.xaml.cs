using EQX.Core.InOut;
using EQX.Core.Motion;
using EQX.InOut;
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
        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {

            if (this.DataContext is CylinderBase cylinder)
            {
                if (Interlock == false)
                {
                    MessageBoxEx.ShowDialog("Interlock Check Error");
                    return;
                }
                if (cylinder.CylinderType == Core.InOut.ECylinderType.ForwardBackwardReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.UpDownReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.RightLeftReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.LockUnlockReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.GripUngripReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.AlignUnalignReverse
                    )
                {
                    cylinder.Backward();
                    return;
                }
                cylinder.Forward();
            }
        }

        private void BackwardButton_Click(object sender, RoutedEventArgs e)
        {

            if (this.DataContext is CylinderBase cylinder)
            {
                if (Interlock == false)
                {
                    MessageBoxEx.ShowDialog("Interlock Check Error");
                    return;
                }
                if (cylinder.CylinderType == Core.InOut.ECylinderType.ForwardBackwardReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.UpDownReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.RightLeftReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.LockUnlockReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.GripUngripReverse ||
                    cylinder.CylinderType == Core.InOut.ECylinderType.AlignUnalignReverse
                    )
                {
                    cylinder.Forward();
                    return;
                }

                cylinder.Backward();
            }
        }
    }
}

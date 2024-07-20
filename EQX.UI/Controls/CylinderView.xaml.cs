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

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is CylinderBase cylinder)
            {
                cylinder.Forward();
            }
        }

        private void BackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is CylinderBase cylinder)
            {
                cylinder.Backward();
            }
        }
    }
}

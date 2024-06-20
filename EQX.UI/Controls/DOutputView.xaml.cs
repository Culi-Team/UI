using EQX.Core.InOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for DOutputView.xaml
    /// </summary>
    public partial class DOutputView : UserControl
    {
        public DOutputView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext == null) return;
            if (this.DataContext.GetType().GetInterfaces().Contains(typeof(IDOutput)) == false) return;

            ((IDOutput)this.DataContext).Value = !((IDOutput)this.DataContext).Value;
        }
    }
}

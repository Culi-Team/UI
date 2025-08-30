using EQX.Core.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EQX.UI.Controls
{
    /// <summary>
    /// Interaction logic for RunModeDialog.xaml
    /// </summary>
    public partial class RunModeDialog : Window
    {
        public RunModeDialog()
        {
            InitializeComponent();
        }
        public static T? ShowRunModeDialog<T>(IEnumerable<T> modes) where T : struct, Enum
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                var vm = new RunModeDialogViewModel<T>(modes);
                var dialog = new RunModeDialog { DataContext = vm };
                vm.RequestClose += () =>
                {
                    dialog.DialogResult = true;
                    dialog.Close();
                };
                var owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
                if (owner != null)
                    dialog.Owner = owner;
                return dialog.ShowDialog() == true ? vm.SelectedMode : (T?)null;
            });
        }
    }
}

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
            if (DataContext is RunModeDialogViewModel vm)
            {
                vm.RequestClose += () =>
                {
                    DialogResult = true;
                    SelectedMode = vm.SelectedMode;
                    Close();
                };
            }
        }
        public EMachineRunMode SelectedMode { get; private set; }

        public static EMachineRunMode? ShowRunModeDialog()
        {
            return Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new RunModeDialog();
                var owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
                if (owner != null)
                    dialog.Owner = owner;
                return dialog.ShowDialog() == true ? dialog.SelectedMode : (EMachineRunMode?)null;
            });
        }
    }
}

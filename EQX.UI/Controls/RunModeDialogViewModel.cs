using CommunityToolkit.Mvvm.Input;
using EQX.Core.Process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Controls
{
    public partial class RunModeDialogViewModel
    {
        public RunModeDialogViewModel()
        {
            SelectModeCommand = new RelayCommand<EMachineRunMode>(mode =>
            {
                SelectedMode = mode;
                RequestClose?.Invoke();
            });
            CloseCommand = new RelayCommand(() => RequestClose?.Invoke());
        }

        public EMachineRunMode SelectedMode { get; private set; }

        public IRelayCommand<EMachineRunMode> SelectModeCommand { get; }

        public IRelayCommand CloseCommand { get; }

        public event Action? RequestClose;
    }
}

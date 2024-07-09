using CommunityToolkit.Mvvm.ComponentModel;
using EQX.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Controls
{
    public class AlarmNotifyViewModel : ObservableObject
    {
        private readonly AlarmNotifyModel alarmNotifyModel;
        public AlarmNotifyViewModel(AlarmNotifyModel alarmNotifyModel)
        {
            this.alarmNotifyModel = alarmNotifyModel;
        }
        public int AlarmId { get => alarmNotifyModel.AlarmId; }
        private string AlarmCode { get => alarmNotifyModel.AlarmCode; } 
        private string AlarmOverviewSource { get => alarmNotifyModel.AlarmOverviewSource; } 
        private string AlarmDetailviewSource { get => alarmNotifyModel.AlarmDetailviewSource; } 
        private List<string> AlarmStepCheck { get => alarmNotifyModel.AlarmStepCheck; } 
    }
}

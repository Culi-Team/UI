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
        private readonly AlarmModel alarmModel;
        public AlarmNotifyViewModel(AlarmModel alarmModel)
        {
            this.alarmModel = alarmModel;
        }
        public int Id { get => alarmModel.Id; }
        private string Code { get => alarmModel.Code; } 
        private string AlarmOverviewSource { get => alarmModel.AlarmOverviewSource; } 
        private string AlarmDetailviewSource { get => alarmModel.AlarmDetailviewSource; } 
        private List<string> TroubleshootingSteps { get => alarmModel.TroubleshootingSteps; } 
    }
}

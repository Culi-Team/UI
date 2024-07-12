using EQX.UI.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Services
{
    public interface IAlarmService
    {
        AlarmModel GetById(int alarmId);
    }
}

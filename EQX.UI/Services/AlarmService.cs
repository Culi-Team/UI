using EQX.UI.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Services
{
    public class AlarmService : IAlarmService
    {
        public AlarmNotifyModel GetById(int alarmId)
        {
            string filePath = @"D:\TOP_UTG\data.txt";
            if (File.Exists(filePath))
            {
                AlarmNotifyModel alarmNotifyModel = new AlarmNotifyModel();
                string text = File.ReadAllText(filePath);
                List<AlarmNotifyModel> alarmNotifyModels = JsonConvert.DeserializeObject<List<AlarmNotifyModel>>(text);
                alarmNotifyModel = alarmNotifyModels.FirstOrDefault(x => x.AlarmId == alarmId);
                
                if (alarmNotifyModel != null)
                {
                    return alarmNotifyModel;
                }
                throw new ArgumentNullException($"Can't find data");
            }
            throw new ArgumentNullException($"Can't file: {filePath}");
        }
    }
}

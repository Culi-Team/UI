using EQX.UI.Dtos;
using Newtonsoft.Json;
using System.IO;

namespace EQX.UI.Services
{
    public class AlarmService : IAlarmService
    {
        public AlarmModel GetById(int alarmId)
        {
            string filePath = @"D:\TOP_UTG\data.txt";
            if (File.Exists(filePath) == false) throw new ArgumentNullException($"Can't file: {filePath}");

            AlarmModel alarmNotifyModel = new AlarmModel();
            string text = File.ReadAllText(filePath);
            List<AlarmModel> alarmNotifyModels = JsonConvert.DeserializeObject<List<AlarmModel>>(text);
            alarmNotifyModel = alarmNotifyModels.FirstOrDefault(x => x.Id == alarmId);

            if (alarmNotifyModel == null) throw new ArgumentNullException($"Can't find data");

            return alarmNotifyModel;
        }
    }
}

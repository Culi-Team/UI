using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Dtos
{
    public class AlarmNotifyModel
    {
        public int AlarmId { get; set; }
        public string AlarmCode { get; set; }
        public string AlarmOverviewSource { get; set; }
        public string AlarmDetailviewSource { get; set; }
        public List<string> AlarmStepCheck { get; set; }
    }
}

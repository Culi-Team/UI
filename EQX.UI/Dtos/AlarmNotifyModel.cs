using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Dtos
{
    public class AlarmModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string AlarmOverviewSource { get; set; }
        public string AlarmDetailviewSource { get; set; }
        public List<string> TroubleshootingSteps { get; set; }
    }
}

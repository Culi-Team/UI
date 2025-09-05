using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Interlock
{
    public class InterlockContext
    {
        public bool IsSafetyDoorClosed { get; set; }
        public bool IsAxisMoving { get; set; }
        public bool IsCylinderOk { get; set; }
    }
}

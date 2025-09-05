using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Interlock
{
    public interface IInterlockRule
    {
        string Key { get; }
        bool IsSatisfied(InterlockContext ctx);
    }
}

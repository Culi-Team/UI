using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Interlock
{
    public class LambdaInterlockRule : IInterlockRule
    {
        private readonly Func<InterlockContext, bool> _predicate;
        public LambdaInterlockRule(string key, Func<InterlockContext, bool> predicate)
        {
            Key = key;
            _predicate = predicate;
        }
        public string Key { get; }
        public bool IsSatisfied(InterlockContext ctx) => _predicate(ctx);
    }
}

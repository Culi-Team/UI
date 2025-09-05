using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EQX.UI.Interlock
{
    public class InterlockService
    {
        private readonly List<IInterlockRule> _rules = new();
        private readonly InterlockContext _context = new();
        public static InterlockService Default { get; } = new();
        public event Action<string, bool>? InterlockChanged;
        public void RegisterRule(IInterlockRule rule) => _rules.Add(rule);
        public void UpdateContext(Action<InterlockContext> updateAction)
        {
            updateAction(_context);
            Evaluate();
        }
        public void Reevaluate() => Evaluate();
        private void Evaluate()
        {
            foreach (var rule in _rules)
            {
                bool satisfied = rule.IsSatisfied(_context);
                InterlockChanged?.Invoke(rule.Key, satisfied);
            }
        }
    }
}

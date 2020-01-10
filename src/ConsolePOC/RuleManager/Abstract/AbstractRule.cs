using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsolePOC.RuleManager.Abstract
{
    public abstract class AbstractRule<T>
    {
        private AbstractRule<T> _next;

        public List<T> Results { get; private set; }
        public AbstractRule<T> SubRule { get; set; }

        public AbstractRule()
        {
            Results = new List<T>();
        }

        public AbstractRule<T> AddNext(AbstractRule<T> rule)
        {
            _next = rule;
            return _next;
        }

        public async Task<List<T>> GetResultList()
        {
            await Execute();

            return Results;
        }

        private async Task Execute()
        {
            T ruleResult = await RuleAction();
            if (ruleResult != null)
            {
                Results.Add(ruleResult);
            }

            if (SubRule != null)
            {
                var subRuleResult = await SubRule.GetResultList();
                Results.AddRange(subRuleResult);
            }

            if (_next != null)
            {
                Results.AddRange(await _next.GetResultList());
            }
        }

        protected abstract Task<T> RuleAction();
    }
}

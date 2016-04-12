using System.Collections.Generic;
using Parser.Interface;

namespace Parser.Collections
{
    /*
    Collection of operators, stored internally in a dictionary, with the symbol being the key


    */
    public class OperatorCollection<T>
    {
        private Dictionary<string, IEvaluatable<T>> Ops { get; } = new Dictionary<string, IEvaluatable<T>>();
        public OperatorCollection(IEnumerable<IEvaluatable<T>> operators)
        {
            foreach (var op in operators)
            {
                Ops.Add(op.Symbol, op);
            }
        }

        public IEvaluatable<T> this[string key] => Ops[key];

        public bool OperatorExists(string symbol)
        {
            return Ops.ContainsKey(symbol);
        }
    }
}
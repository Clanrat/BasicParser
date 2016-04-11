using System.Collections.Generic;
using Parser.Interface;

namespace Parser.Converter
{
    /*
    Collection of operators, stored internally in a dictionary, with the symbol being the key


    */
    public class OperatorCollection<T>
    {
        private Dictionary<string, IOperator<T>> Ops { get; } = new Dictionary<string, IOperator<T>>();
        public OperatorCollection(IEnumerable<IOperator<T>> operators)
        {
            foreach (var op in operators)
            {
                Ops.Add(op.Symbol, op);
            }
        }

        public IOperator<T> this[string key] => Ops[key];

        public bool OperatorExists(string symbol)
        {
            return Ops.ContainsKey(symbol);
        }
    }
}
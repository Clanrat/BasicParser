using System.Collections.Generic;
using Parser.Interface;

namespace Parser.Converter
{
    public class OperatorCollection
    {
        private Dictionary<string, IOperator> Ops { get; } = new Dictionary<string, IOperator>();
        public OperatorCollection(IEnumerable<IOperator> operators)
        {

            foreach (var op in operators)
            {
                Ops.Add(op.Symbol, op);
            }
        }

        public IOperator this[string key] => Ops[key];

        public bool OperatorExists(string symbol)
        {
            return Ops.ContainsKey(symbol);
        }
    }
}
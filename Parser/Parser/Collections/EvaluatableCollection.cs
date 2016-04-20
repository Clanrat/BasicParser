using System;
using System.Collections;
using System.Collections.Generic;
using Parser.Interface;

namespace Parser.Collections
{
    /*
    Collection of Evaluatables, stored internally in a dictionary, with the symbol being the key


    */
    public class EvaluatableCollection<T> : ICollection<KeyValuePair<string, IEvaluatable<T>>>
    {
        private Dictionary<string, IEvaluatable<T>> Ops { get; } = new Dictionary<string, IEvaluatable<T>>();
        public EvaluatableCollection(IEnumerable<IEvaluatable<T>> operators)
        {
            foreach (var op in operators)
            {
                Add(new KeyValuePair<string, IEvaluatable<T>>(op.Symbol, op));
            }
        }

        public IEvaluatable<T> this[string key] => Ops[key];

        public bool OperatorExists(string symbol)
        {
            return Ops.ContainsKey(symbol);
        }

        public IEnumerator<KeyValuePair<string, IEvaluatable<T>>> GetEnumerator()
        {
            return Ops.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<string, IEvaluatable<T>> item)
        {
            if(!OperatorExists(item.Key))
                Ops.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            Ops.Clear();
        }

        public bool Contains(KeyValuePair<string, IEvaluatable<T>> item)
        {
            return OperatorExists(item.Key);
        }

        public void CopyTo(KeyValuePair<string, IEvaluatable<T>>[] array, int arrayIndex)
        {
            
            if(array.Length - arrayIndex < Ops.Count)
                throw new IndexOutOfRangeException("The destination array is not sufficiently large");

            foreach (var kvp in Ops)
            {
                array[arrayIndex] = kvp;
                ++arrayIndex;
            }
        }

        public bool Remove(KeyValuePair<string, IEvaluatable<T>> item)
        {
            return Ops.Remove(item.Key);
        }

        public int Count => Ops.Count;

        public bool IsReadOnly { get; } = true;
    }
}
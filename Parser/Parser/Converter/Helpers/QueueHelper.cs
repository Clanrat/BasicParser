using System.CodeDom;
using System.Collections.Generic;

namespace Parser.Converter.Helpers
{
    internal static class QueueHelper
    {

        public static void RemoveFromQueue<T>(int count, Queue<T> queue)
        {
            for (var i = 0; i < count; i ++)
                queue.Dequeue();
        }
    
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Errata.Collections
{
   public class RemainderLast<T> : IEnumerable<T>
    {
        public RemainderLast(IEnumerable<T> items)
        {
            var list = items.ToList();
            if (list.Count == 0)
                throw new Exception("Enumeration must have at least 1 item");

 
            Last = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            Remainder = list;
        }

        public List<T > Remainder { get; set; }
        public T Last { get; set; }
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in Remainder)
                yield return item;
            yield return Last;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (var item in Remainder)
                yield return item;
            yield return Last;
        }
    }
}

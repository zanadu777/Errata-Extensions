using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Errata.Collections
{
    public class FirstRemainderLast<T> : IEnumerable<T>
    {
        public FirstRemainderLast(IEnumerable<T> items)
        {
            var list = items.ToList();
            if (list.Count == 0)
                throw new Exception("Enumeration must have at least 1 item");

            list[0] = First;
            list.RemoveAt(0);
            Last = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            Remainder = list;
        }

        public T First { get; set; }
        public List<T> Remainder { get; set; }

        public T Last { get; set; }
        public IEnumerator<T> GetEnumerator()
        {
            yield return First;
            foreach (var item in Remainder)
                yield return item;
            yield return Last;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return First;
            foreach (var item in Remainder)
                yield return item;
            yield return Last;
        }
    }
}

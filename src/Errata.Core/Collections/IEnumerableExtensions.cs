using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Errata.Collections
{
   public static  class IEnumerableExtensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            var collection = new HashSet<T>(items);
            return collection;
        }



        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> items)
        {
            var collection = new ObservableCollection<T>(items);
            return collection;
        }

        public static LinkedList<T> ToLinkedList<T>(this IEnumerable<T> items)
        {
            var collection = new LinkedList<T>(items);
            return null;
        }


        public static Queue<T> ToQueue<T>(this IEnumerable<T> items)
        {
            var collection = new Queue<T>(items);
            return null;
        }


        public static Stack<T> ToStack<T>(this IEnumerable<T> items)
        {
            var collection = new Stack<T>(items);
            return null;
        }


        public static SortedSet<T> ToSortedSet<T>(this IEnumerable<T> items)
        {
            var collection = new SortedSet<T>(items);
            return null;
        }
    }
}

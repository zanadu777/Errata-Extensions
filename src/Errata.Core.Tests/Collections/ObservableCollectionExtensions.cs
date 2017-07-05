using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Errata.Collections
{
    public static class ObservableCollectionExtensions
    {

        public static void ReplaceContents<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            collection.AddRange(items);
        }

    }
}

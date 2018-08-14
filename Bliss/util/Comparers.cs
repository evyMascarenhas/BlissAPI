using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bliss.util
{
    public static class Comparers
    {
        public static bool In<T>(this T source, params T[] list)
        {
            return list.Contains(source);
        }

        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
                                                                                            Func<T, TKey> getKey)
        {
            return from item in items
                   join otherItem in other on getKey(item)
                   equals getKey(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;

        }

        public static IEnumerable<t> DistinctBy<t>(this IEnumerable<t> list, Func<t, object> propertySelector)
        {
            return list.GroupBy(propertySelector).Select(x => x.First());
        }
    }
}
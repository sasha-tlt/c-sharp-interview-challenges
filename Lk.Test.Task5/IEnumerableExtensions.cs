namespace Lk.Test.Task5
{
    using System;
    using System.Collections.Generic;

    public static class IEnumerableExtensions
    {
        public static int Count<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            if (collection == null)
            {
                throw new ArgumentException("Коллекция не задана");
            }

            if (predicate == null)
            {
                throw new ArgumentException("Предикат не задан");
            }

            int counter = 0;
            foreach(var element in collection)
            {
                if (predicate(element))
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}

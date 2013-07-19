namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;

    public static class IEnumerableExtensions
    {
        private static readonly Random r = new Random();
        /// <summary>
        /// Returns a random item;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="rg"></param>
        /// <returns></returns>
        public static T Random<T>(this IList<T> list, Random rg)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            if (rg == null)
            {
                throw new ArgumentNullException("rg");
            }

            int index = rg.Next(list.Count);
            return list[index];
        }

        /// <summary>
        /// Returns a random item;
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="rg"></param>
        /// <returns></returns>
        public static T Random<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            int index = r.Next(list.Count);
            return list[index];
        }
    }
}

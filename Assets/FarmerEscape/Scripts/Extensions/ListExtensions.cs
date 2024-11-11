using System;
using System.Collections.Generic;

namespace Game.Scripts.Core
{
    public static class ListExtensions
    {
        private static Random _rnd = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = _rnd.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }

        public static List<T> GetClone<T>(this List<T> source)
        {
            return source.GetRange(0, source.Count);
        }
    }
}

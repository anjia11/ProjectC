using System;
using System.Collections.Generic;

namespace _Scripts.Behavior_Tree.Utils
{
    public static class Utils
    {
        public static Random r = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = r.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}
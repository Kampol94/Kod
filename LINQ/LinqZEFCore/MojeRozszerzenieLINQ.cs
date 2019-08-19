using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LinqZEFCore
{
    public static class MojeRozszerzenieLINQ 
    {
        public static IEnumerable<T> PrzetwarzajSekwencje<T>(this IEnumerable<T> sekwencja)
        {
            return sekwencja;
        }

        public static int? Median(this IEnumerable<int?> sequence)
        {
            var ordered = sequence.OrderBy(item => item);
            int middlePosition = ordered.Count() / 2;
            return ordered.ElementAt(middlePosition);
        }
        public static int? Median<T>(
            this IEnumerable<T> sequence, Func<T, int?> selector)
        {
            return sequence.Select(selector).Median();
        }

        public static decimal? Median(this IEnumerable<decimal?> sequence)
        {
            var ordered = sequence.OrderBy(item => item);
            int middlePosition = ordered.Count() / 2;
            return ordered.ElementAt(middlePosition);
        }

        public static decimal? Median<T>(
            this IEnumerable<T> sequence, Func<T, decimal?> selector)
        {
            return sequence.Select(selector).Median();
        }

        public static int? Mode(this IEnumerable<int?> sequence)
        {
            var grouped = sequence.GroupBy(item => item);
            var orderedGroups = grouped.OrderBy(group => group.Count());
            return orderedGroups.FirstOrDefault().Key;
        }

        public static int? Mode<T>(
            this IEnumerable<T> sequence, Func<T, int?> selector)
        {
            return sequence.Select(selector).Mode();
        }

        public static decimal? Mode(this IEnumerable<decimal?> sequence)
        {
            var grouped = sequence.GroupBy(item => item);
            var orderedGroups = grouped.OrderBy(group => group.Count());
            return orderedGroups.FirstOrDefault().Key;
        }

        public static decimal? Mode<T>(
            this IEnumerable<T> sequence, Func<T, decimal?> selector)
        {
            return sequence.Select(selector).Mode();
        }

    }
}

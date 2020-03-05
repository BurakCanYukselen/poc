using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsolePOC.Extensions
{
    public static class ListExtensions
    {

        public static IEnumerable<(TLeft Left, TRight Right)> LeftJoin<TLeft, TRight, TKey>(this IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TKey> leftKeySelector, Func<TRight, TKey> rightKeySelector)
        {
            return left.Join(right, leftKeySelector, rightKeySelector, (leftList, rightList) => (Left: leftList, Right: rightList))
                       .Concat(left.GroupJoin(right, leftKeySelector, rightKeySelector, (leftList, rightList) => new { leftList, rightList })
                                   .Where(p => !p.rightList.Any())
                                   .Select(p => (Left: p.leftList, Right: p.rightList.FirstOrDefault())));
        }

        public static IEnumerable<(TLeft Left, TRight Right)> InnerJoin<TLeft, TRight, TKey>(this IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TKey> leftKeySelector, Func<TRight, TKey> rightKeySelector)
        {
            return left.Join(right, leftKeySelector, rightKeySelector, (leftList, rightList) => (Left: leftList, Right: rightList));
        }

        public static IEnumerable<TSource> GetDataExistsIn<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> target, Func<TSource, TSource> selector)
        {
            Predicate<TSource> predicate = property => target.Contains(property);
            return source.Where(p => predicate(selector.Invoke(p)));
        }

        public static string ToJson(this object source, Formatting formatting = Formatting.Indented)
        {
            return JsonConvert.SerializeObject(source, formatting);
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
        {
            return source.GroupBy(selector).Select(p => p.First());
        }
    }
}

using System.Linq;
using System;
using System.Collections.Generic;
namespace Core.Lib.Decorator
{

    internal static class Helper
    {
        internal static Action Throw<TException>(this string msg, Func<string, TException> exception)
          where TException : Exception
          => () => throw exception(msg);
        
        internal static string JoinBy(this IEnumerable<string> strs, string separator)
            => string.Join(separator, strs);
        internal static void Each<T>(this IEnumerable<T> seq, Action<T> action)
        {
            foreach (var t in seq)
            {
                action(t);
            }
        }
        internal static IEnumerable<TSource> GetAll<TSource, TResult>(this TSource t, Func<TSource, IEnumerable<TResult>> selector, Func<TResult, TSource> next)
          => selector(t).Select(next).GetAll(selector, next);

        internal static IEnumerable<T> GetAll<T>(this T t, Func<T, IEnumerable<T>> selector)
            => t.GetAll(selector, x => x);

        internal static IEnumerable<TSource> GetAll<TSource, TResult>(this IEnumerable<TSource> ts, Func<TSource, IEnumerable<TResult>> selector, Func<TResult, TSource> next)
            => ts.Any()
                ? ts.GetAll(selector, next)
                : ts;
    }
}

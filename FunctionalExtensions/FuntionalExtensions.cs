using FunctionalExtensions.PatternMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalExtensions
{
    public static class FuntionalExtensions
    {
        public static TOut Map<TIn, TOut>(this TIn input, Func<TIn, TOut> mapper)
        {
            return mapper(input);
        }

        public static TOut TryMap<TIn, TOut>(this TIn input, Func<TIn, TOut> mapper, TOut defaultVal = default(TOut))
        {
            return input != null ? mapper(input) : defaultVal;
        }

        public static TIn Exec<TIn>(this TIn input, Action<TIn> action)
        {
            action(input);
            return input;
        }

        public static TIn TryExec<TIn>(this TIn input, Action<TIn> action)
        {
            if (input != null) action(input);
            return input;
        }

        public static ThenBuilder<TIn> Case<TIn>(this TIn input, Predicate<TIn> predicate)
        {
            return new ThenBuilder<TIn>(input, predicate(input) ? MatchType.ThisMatched : MatchType.None);
        }


        public static ThenBuilder<TIn> Case<TIn>(this TIn input, TIn value)
        {
            return input.Case(i => i.Equals(value));
        }

        public static T Cast<T>(this object target)
        {
            return (T) target;
        }

        public static bool IsNull<T>(this T target)
        {
            return target == null;
        }

        public static bool IsNotNull<T>(this T target)
        {
            return target != null;
        }

        public static bool IsNullOrEmpty(this string target)
        {
            return string.IsNullOrEmpty(target);
        }

        public static DateTime ToDate(this string dateStr, string format = null)
        {
            return format
                .Case(f => f.IsNullOrEmpty()).Then(DateTime.Parse(dateStr))
                .Otherwise(DateTime.ParseExact(dateStr, format, null));
        }
    }
}

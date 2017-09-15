using System;
using System.Collections.Generic;

namespace RxAPI.Observables
{
    public static class ObservableExtensions
    {
        public static IObservable<T> ToObservable<T>(
            this IEnumerable<T> src)
            => new ToObservable<T>(src);

        public static IObservable<T> Where<T>(
            this IObservable<T> src,
            Func<T, bool> filterFn)
            => new Where<T>(src, filterFn);

        public static IObservable<U> Select<T, U>(
            this IObservable<T> src,
            Func<T, U> selectorFn)
            => new Select<T, U>(src, selectorFn);
    }
}

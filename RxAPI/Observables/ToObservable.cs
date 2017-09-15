using System;
using System.Collections.Generic;
using RxAPI.Disposables;

namespace RxAPI.Observables
{
    public class ToObservable<T> : IObservable<T>
    {
        private IEnumerable<T> _values;

        public ToObservable(IEnumerable<T> value) => _values = value;

        public IDisposable Subscribe(IObserver<T> observer)
        {
            foreach (var value in _values)
            {
                observer.OnNext(value);
            }
            observer.OnCompleted();
            return Disposable.Empty;
        }
    }
}

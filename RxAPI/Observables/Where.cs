using System;
using RxAPI.Disposables;

namespace RxAPI.Observables
{
    public class Where<T> : IObservable<T>, IObserver<T>
    {
        private IObservable<T> _src;
        private readonly Func<T, bool> _filterFn;
        private IObserver<T> _observer;

        public Where(
            IObservable<T> src,
            Func<T, bool> filterFn)
        {
            _src = src;
            _filterFn = filterFn;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            _observer = observer;
            return new CompositeDisposable
            {
                _src.Subscribe(this),
                Disposable.Create(() => _observer = null)
            };
        }

        public void OnNext(T value)
        {
            if (_filterFn(value)) _observer.OnNext(value);
        }

        public void OnError(Exception error) => _observer.OnError(error);

        public void OnCompleted() => _observer.OnCompleted();
    }
}

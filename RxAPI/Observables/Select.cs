using System;
using RxAPI.Disposables;

namespace RxAPI.Observables
{
    public class Select<TIn, TOut> : IObservable<TOut>, IObserver<TIn>
    {
        private IObservable<TIn> _src;
        private Func<TIn, TOut> _selectorFn;
        private IObserver<TOut> _observer;

        public Select(
            IObservable<TIn> src,
            Func<TIn, TOut> selectorFn)
        {
            _src = src;
            _selectorFn = selectorFn;
        }

        public IDisposable Subscribe(IObserver<TOut> observer)
        {
            _observer = observer;
            return new CompositeDisposable
            {
                _src.Subscribe(this),
                Disposable.Create(() => _observer = null)
            };
        }

        public void OnNext(TIn value) => _observer.OnNext(_selectorFn(value));

        public void OnError(Exception error) => _observer.OnError(error);

        public void OnCompleted() => _observer.OnCompleted();
    }
}

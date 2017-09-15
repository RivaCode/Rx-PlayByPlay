using System;

namespace RxAPI.Disposables
{
    public class Disposable
    {
        public static IDisposable Empty { get; } = Create(() => { });

        public static IDisposable Create(Action disposeAction) => new AnonymousDisposable(disposeAction);

        private class AnonymousDisposable : IDisposable
        {
            private readonly Action _disposeAction;

            public AnonymousDisposable(Action disposeAction) => _disposeAction = disposeAction;
            public void Dispose() => _disposeAction();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RxAPI.Disposables
{
    public class CompositeDisposable : IDisposable, ICollection<IDisposable>
    {
        private readonly List<IDisposable> _disposables;

        public CompositeDisposable(): this(Enumerable.Empty<IDisposable>()) { }
        public CompositeDisposable(IEnumerable<IDisposable> disposables) => _disposables = disposables.ToList();

        public void Dispose() => _disposables.ForEach(d => d.Dispose());

        public IEnumerator<IDisposable> GetEnumerator() => _disposables.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public void Add(IDisposable item) => _disposables.Add(item);

        public void Clear() => _disposables.Clear();

        public bool Contains(IDisposable item) => _disposables.Contains(item);

        public void CopyTo(IDisposable[] array, int arrayIndex) => _disposables.CopyTo(array, arrayIndex);

        public bool Remove(IDisposable item) => _disposables.Remove(item);

        public int Count => _disposables.Count;
        public bool IsReadOnly => false;
    }
}

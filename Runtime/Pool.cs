using System;
using System.Collections.Generic;

namespace Utils
{
    public class Pool<T> : IPool<T>
    {
        private readonly Stack<T> _stack = new();

        public Func<T> CreateFunc;
        public Action<T> OnBeforeGet { get; set; }
        public Action<T> OnBeforeReturn { get; set; }
        public Pool(Func<T> createFunc = null, int preload = 0)
        {
            CreateFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));

            for (int i = 0; i < preload; i++)
            {
                var item = Create();
                _stack.Push(item);
            }
        }

        public T Get()
        {
            T item = _stack.Count > 0 ? _stack.Pop() : Create();
            OnBeforeGet?.Invoke(item);
            if (item is IPoolable p)
            {
                p.OnTakenFromPool();
            }
            return item;
        }

        public void Release(T item)
        {
            if (item is IPoolable p)
            {
                p.OnReturnedToPool();
            }
            OnBeforeReturn?.Invoke(item);
            _stack.Push(item);
        }

        private T Create()
        {
            return CreateFunc();
        }
    }
}
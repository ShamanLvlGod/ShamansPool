using System;

namespace Utils
{
    public interface IPool<T>
    {
        T Get();
        void Release(T item);
        
        /// <summary>
        /// Optional hook called before an item is returned to the pool.
        /// </summary>
        Action<T> OnBeforeReturn { get; set; }

        /// <summary>
        /// Optional hook called after an item is retrieved from the pool.
        /// </summary>
        Action<T> OnBeforeGet { get; set; }
    }
}
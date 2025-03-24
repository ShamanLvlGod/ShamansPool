using System;
using UnityEngine;

namespace Utils
{
    public abstract class BaseUnityPool<T> : MonoBehaviour, IPool<T> where T : Component
    {
        [Header("Pool Settings")] [SerializeField]
        private int preloadAmount = 0;

        [SerializeField] private Transform poolRoot;

        private Pool<T> _pool;

        public Action<T> OnBeforeGet
        {
            get => _pool.OnBeforeGet;
            set => _pool.OnBeforeGet = value;
        }

        public Action<T> OnBeforeReturn
        {
            get => _pool.OnBeforeReturn;
            set => _pool.OnBeforeReturn = value;
        }

        protected virtual void Awake()
        {
            if (GetArchetype() == null)
            {
                Debug.LogError($"[{GetType().Name}] No valid prefab found.");
                enabled = false;
                return;
            }

            _pool = new Pool<T>(
                createFunc: CreateInstance,
                preload: preloadAmount
            );
        }

        public T Get()
        {
            T item = _pool.Get();
            item.transform.SetParent(null, false);
            item.gameObject.SetActive(true);
            return item;
        }

        public void Release(T item)
        {
            item.gameObject.SetActive(false);
            if (poolRoot != null)
                item.transform.SetParent(poolRoot, false);
            _pool.Release(item);
        }

        private T CreateInstance()
        {
            var obj = Instantiate(GetArchetype(), poolRoot);
            obj.gameObject.SetActive(false);
            return obj;
        }

        protected abstract T GetArchetype();
    }
}
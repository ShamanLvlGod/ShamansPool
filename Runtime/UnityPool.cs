using UnityEngine;

namespace Utils
{
    public class UnityPool<T> : MonoBehaviour, IPool<T> where T : Component
    {
        [Header("Pool Settings")]
        [SerializeField] private T prefab;
        [SerializeField] private int preloadAmount = 0;
        [SerializeField] private Transform poolRoot;

        private Pool<T> _pool;

        private void Awake()
        {
            if (prefab == null)
            {
                Debug.LogError($"[{nameof(UnityPool<T>)}] Prefab not assigned.");
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
            var obj = Instantiate(prefab, poolRoot);
            obj.gameObject.SetActive(false);
            return obj;
        }
    }
}
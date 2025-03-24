using UnityEngine;

namespace Utils
{
    public class UnityPool<T> : BaseUnityPool<T> where T : Component
    {
        [SerializeField] private T archetype;

        protected override T GetArchetype()
        {
            return archetype;
        }
    }
}
using UnityEngine;

namespace Utils
{
    public class RoundRobinUnityPool<T> : BaseUnityPool<T> where T : Component
    {
        [SerializeField] protected T[] archetypes;
        private int _currentIndex = 0;

        protected override T GetArchetype()
        {
            if (archetypes == null || archetypes.Length == 0) return null;
            T archetype = archetypes[_currentIndex];
            _currentIndex = (_currentIndex + 1) % archetypes.Length;
            return archetype;
        }
        public void SetArchetypes(T[] availableTypes)
        {
            archetypes = availableTypes;
        }
        
    }
}
namespace Utils
{
    public interface IPoolable
    {
        void OnTakenFromPool();
        void OnReturnedToPool();
    }
}
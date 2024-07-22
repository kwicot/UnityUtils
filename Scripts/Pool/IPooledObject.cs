namespace Core.Scripts.Pool
{
    public interface IPooledObject
    {
        public void OnReuse();
        public void OnRelease();
    }
}
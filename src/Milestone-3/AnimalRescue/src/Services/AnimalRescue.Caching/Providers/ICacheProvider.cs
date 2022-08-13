namespace AnimalRescue.Caching.Providers
{
    public interface ICacheProvider
    {

        T Add<T>(string key, T obj);

        T Get<T>(string key);

        bool Remove(string keyj);

    }
}

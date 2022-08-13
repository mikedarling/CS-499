using AnimalRescue.Caching.Providers;

namespace AnimalRescue.Caching.Managers
{

    /// <summary>
    /// Cache Manager that manages Animal objects.
    /// </summary>
    public class AnimalCacheManager : BaseCacheManager
    {

        #region Constructor

        public AnimalCacheManager(ICacheProvider cacheProvider)
            : base("Animals", cacheProvider)
        {
        }

        #endregion

    }
}

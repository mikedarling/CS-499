using AnimalRescue.Caching.Providers;

namespace AnimalRescue.Caching.Managers
{
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

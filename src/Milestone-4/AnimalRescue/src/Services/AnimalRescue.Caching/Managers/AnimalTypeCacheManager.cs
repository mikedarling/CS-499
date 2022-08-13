using AnimalRescue.Caching.Providers;

namespace AnimalRescue.Caching.Managers
{

    /// <summary>
    /// Cache Manager that manages AnimalType objects.
    /// </summary>
    public class AnimalTypeCacheManager : BaseCacheManager
    {

        #region Constructor

        public AnimalTypeCacheManager(ICacheProvider cacheProvider)
            : base("AnimalTypes", cacheProvider)
        {
        }

        #endregion

    }
}

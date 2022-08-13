using AnimalRescue.Caching.Providers;

namespace AnimalRescue.Caching.Managers
{

    /// <summary>
    /// Cache Manager that manages Breed objects.
    /// </summary>
    public class BreedCacheManager : BaseCacheManager
    {

        #region Constructors

        public BreedCacheManager(ICacheProvider cacheProvider)
            : base("Breed", cacheProvider)
        {
        }

        #endregion

    }
}

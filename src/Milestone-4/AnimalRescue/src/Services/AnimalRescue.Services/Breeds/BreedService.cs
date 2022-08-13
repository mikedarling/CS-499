using AnimalRescue.Caching.Managers;
using AnimalRescue.Data.Models.DomainModels;
using AnimalRescue.Data.Models.Entities;
using AnimalRescue.Data.Repositories.DataRepos;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalRescue.Services.Breeds
{

    /// <summary>
    /// Executes all business logic for Breeds.
    /// </summary>
    public class BreedService : IBreedService
    {

        #region Constructors

        public BreedService(IReadableDataRepository readableDataRepository, BreedCacheManager breedCacheManager, IMapper mapper)
        {
            this._readableRepository = readableDataRepository;
            this._breedCacheManager = breedCacheManager;
            this._mapper = mapper;
        }

        #endregion

        #region Local Variables

        private readonly IReadableDataRepository _readableRepository;

        private readonly BreedCacheManager _breedCacheManager;

        private readonly IMapper _mapper;

        private const string BREEDS_CACHE_KEY = "breeds";

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Enumerable of all Breeds from the cache if it's available or from the repository.
        /// </summary>
        /// <returns>An enumerable of <see cref="Breed"/>.</returns>
        public async Task<IEnumerable<BreedModel>> GetBreeds()
        {
            var entities = this._breedCacheManager.Get<List<Breed>>(BREEDS_CACHE_KEY);
            if (entities == null || !entities.Any())
            {
                var result = await this._readableRepository.GetAllAsync<Breed>();
                if (result == null || !result.Any())
                {
                    return null;
                }

                entities = result
                    .OrderBy(x => x.Name)
                    .ToList();

                this._breedCacheManager.Add(BREEDS_CACHE_KEY, entities);
            }

            return this._mapper.Map<IEnumerable<BreedModel>>(entities);
        }

        #endregion

    }
}

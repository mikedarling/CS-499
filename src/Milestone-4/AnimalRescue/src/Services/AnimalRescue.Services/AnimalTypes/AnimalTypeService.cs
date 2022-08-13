using AnimalRescue.Caching.Managers;
using AnimalRescue.Data.Models.DomainModels;
using AnimalRescue.Data.Models.Entities;
using AnimalRescue.Data.Repositories.DataRepos;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnimalRescue.Services.AnimalTypes
{

    /// <summary>
    /// Executes all business logic for AnimalTypes.
    /// </summary>
    public class AnimalTypeService : IAnimalTypeService
    {

        #region Constructors

        public AnimalTypeService(IReadableDataRepository readableRepository, AnimalTypeCacheManager animalTypeCacheManager, IMapper mapper)
        {
            this._readableRepository = readableRepository;
            this._animalTypeCacheManager = animalTypeCacheManager;
            this._mapper = mapper;
        }

        #endregion

        #region Local Variables

        private readonly IReadableDataRepository _readableRepository;

        private readonly AnimalTypeCacheManager _animalTypeCacheManager;

        private readonly IMapper _mapper;

        private const string ANIMAL_TYPES_CACHE_KEY = "animalsTypes";

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Enumerable of all Animals from the cache if it's available or from the repository.
        /// </summary>
        /// <returns>An enumerable of <see cref="AnimalTypeModel"/>.</returns>
        public async Task<IEnumerable<AnimalTypeModel>> GetAnimalTypes()
        {
            var entities = this._animalTypeCacheManager.Get<List<AnimalType>>(ANIMAL_TYPES_CACHE_KEY);
            if (entities == null || !entities.Any())
            {
                var result = await this._readableRepository.GetAllAsync<AnimalType>();
                if (result == null || !result.Any())
                {
                    return null;
                }

                entities = result
                     .OrderBy(x => x.Name)
                     .ToList();

                this._animalTypeCacheManager.Add(ANIMAL_TYPES_CACHE_KEY, entities);
            }

            return this._mapper.Map<IEnumerable<AnimalTypeModel>>(entities);
        }

        #endregion

    }
}

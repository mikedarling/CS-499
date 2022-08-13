using AnimalRescue.Caching.Managers;
using AnimalRescue.Data.Models.DomainModels;
using AnimalRescue.Data.Models.Entities;
using AnimalRescue.Data.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AnimalRescue.Services.Animals
{
    public class AnimalService : IAnimalService
    {

        #region Constructors

        public AnimalService(IReadableDataRepository repository, AnimalCacheManager animalCacheManager,  IMapper mapper)
        {
            this._repository = repository;
            this._animalCacheManager = animalCacheManager;
            this._mapper = mapper;
        }

        #endregion

        #region Local Variables

        private readonly IReadableDataRepository _repository;

        private readonly AnimalCacheManager _animalCacheManager;
        
        private readonly IMapper _mapper;

        private const string ANIMALS_CACHE_KEY = "animals";

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Enumerable of all Animals from the cache if it's available or from the repository.
        /// </summary>
        /// <returns>An enumerable of Animals.</returns>
        public IEnumerable<AnimalModel> GetAnimals()
        {
            var entities = this._animalCacheManager.Get<List<Animal>>(ANIMALS_CACHE_KEY);

            if (entities == null || !entities.Any())
            {
               entities = this._repository.GetAll<Animal>()
                    .ToList();
            }

            if (entities == null || !entities.Any())
            {
                return null;
            }

            this._animalCacheManager.Add(ANIMALS_CACHE_KEY, entities);

            return this._mapper.Map<IEnumerable<AnimalModel>>(entities);
        }

        #endregion

    }
}

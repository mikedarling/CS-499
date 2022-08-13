using AnimalRescue.Caching.Managers;
using AnimalRescue.Data.Models.DomainModels;
using AnimalRescue.Data.Models.Entities;
using AnimalRescue.Data.Repositories.DataRepos;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnimalRescue.Services.Animals
{

    /// <summary>
    /// Executes all business logic for Animals.
    /// </summary>
    public class AnimalService : IAnimalService
    {

        #region Constructors

        public AnimalService(IReadableDataRepository readableRepository, IWriteableDataRepository writeableRepository, AnimalCacheManager animalCacheManager, IMapper mapper)
        {
            this._readableRepository = readableRepository;
            this._writeableRepository = writeableRepository;
            this._animalCacheManager = animalCacheManager;
            this._mapper = mapper;
        }

        #endregion

        #region Local Variables

        private readonly IReadableDataRepository _readableRepository;

        private readonly IWriteableDataRepository _writeableRepository;

        private readonly AnimalCacheManager _animalCacheManager;

        private readonly IMapper _mapper;

        private const string ANIMALS_CACHE_KEY = "animals";

        #endregion

        #region Methods

        /// <summary>
        /// Gets the complete list of animals from the cache if it's available or from the repository.
        /// </summary>
        /// <returns>An enumerable of <see cref="AnimalModel"/>.</returns>
        public async Task<IEnumerable<AnimalModel>> GetAnimals()
        {
            var entities = this._animalCacheManager.Get<List<Animal>>(ANIMALS_CACHE_KEY);
            if (entities == null || !entities.Any())
            {
                var result = await this._readableRepository.GetAllAsync<Animal>();
                if (result == null || !result.Any())
                {
                    return null;
                }

                entities = result
                     .OrderBy(x => x.AnimalId)
                     .ToList();

                this._animalCacheManager.Add(ANIMALS_CACHE_KEY, entities);
            }

            return this._mapper.Map<IEnumerable<AnimalModel>>(entities);
        }

        /// <summary>
        /// Gets the editable detail model for the provided Animal ID.
        /// </summary>
        /// <param name="animalId">The <see cref="Animal.AnimalId" /> of the requested Animal.</param>
        /// <returns>The <see cref="AnimalDetailModel"/> </returns>
        public async Task<AnimalDetailModel> GetAnimalDetailByAnimalId(string animalId)
        {
            var entity = await this._readableRepository.GetAsync<Animal>(x => x.AnimalId == animalId);
            if (entity == null)
            {
                return null;
            }

            return this._mapper.Map<AnimalDetailModel>(entity);
        }

        /// <summary>
        /// Updates the editable details of the provided Animal.
        /// </summary>
        /// <param name="model">The <see cref="AnimalDetailModel"/> with the edited properties.</param>
        /// <returns>The updated <see cref="AnimalModel"/>.</returns>
        public async Task<AnimalModel> UpdateAnimal(AnimalDetailModel model)
        {
            var entity = await this._readableRepository.GetAsync<Animal>(model.Id);
            if (entity == null)
            {
                return null;
            }

            entity.AnimalId = model.AnimalId;
            entity.Name = model.Name;
            entity.IsMixedBreed = model.Mix;

            var animalType = await this.GetAnimalTypeAsync(model.AnimalTypeId);
            if (animalType != null)
            {
                entity.AnimalType = animalType;
            }

            var breeds = await this.GetBreedsAsync(model.BreedIds);
            if (breeds != null && breeds.Any())
            {
                entity.Breeds = breeds
                    .ToList();
            }

            entity = await this._writeableRepository.UpdateAsync<Animal>(entity);

            this._animalCacheManager.Remove(ANIMALS_CACHE_KEY);

            return (entity != null) ? this._mapper.Map<AnimalModel>(entity) : null;
        }

        #endregion

        #region Helpers

        private async Task<AnimalType> GetAnimalTypeAsync(long id)
        {
            if (id == 0)
            {
                return null;
            }

            return await this._readableRepository.GetAsync<AnimalType>(id);
        }

        private async Task<IQueryable<Breed>> GetBreedsAsync(long[] ids)
        {
            if (ids == null || !ids.Any())
            {
                return null;
            }

            return await this._readableRepository.GetManyAsync<Breed>(new Expression<Func<Breed, bool>>[] { (x => ids.Contains(x.Id)) });
        }

        #endregion

    }
}

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

        public AnimalService(IDataRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        #endregion

        #region Local Variables

        private readonly IDataRepository _repository;

        private readonly IMapper _mapper;

        #endregion

        #region Methods

        public IEnumerable<AnimalModel> GetAnimals()
        {
            var entities = this._repository.GetRecords<Animal>();
            if (entities == null || !entities.Any())
            {
                return null;
            }

            return this._mapper.Map<IEnumerable<AnimalModel>>(entities);
        }

        #endregion

    }
}

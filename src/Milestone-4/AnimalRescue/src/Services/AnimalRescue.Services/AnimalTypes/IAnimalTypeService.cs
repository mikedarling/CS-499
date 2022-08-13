using AnimalRescue.Data.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalRescue.Services.AnimalTypes
{
    public interface IAnimalTypeService
    {

        Task<IEnumerable<AnimalTypeModel>> GetAnimalTypes();

    }
}

using AnimalRescue.Data.Models.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnimalRescue.Services.Animals
{
    public interface IAnimalService
    {

        Task<IEnumerable<AnimalModel>> GetAnimals();

        Task<AnimalDetailModel> GetAnimalDetailByAnimalId(string animalId);

        Task<AnimalModel> UpdateAnimal(AnimalDetailModel viewModel);

    }
}

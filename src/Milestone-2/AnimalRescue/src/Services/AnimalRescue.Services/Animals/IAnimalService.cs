using AnimalRescue.Data.Models.DomainModels;
using System.Collections.Generic;

namespace AnimalRescue.Services.Animals
{
    public interface IAnimalService
    {
        IEnumerable<AnimalModel> GetAnimals();
    }
}

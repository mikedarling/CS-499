using AnimalRescue.Data.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalRescue.Services.Interfaces
{
    public interface IAnimalService
    {
        List<AnimalModel> GetAnimals();
        
    }
}

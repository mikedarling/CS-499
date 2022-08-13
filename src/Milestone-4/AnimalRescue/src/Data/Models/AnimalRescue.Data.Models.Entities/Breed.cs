using System.ComponentModel.DataAnnotations;

namespace AnimalRescue.Data.Models.Entities
{
    public class Breed : BaseEntity
    {

        [Required]
        public virtual AnimalType AnimalType { get; set; }

    }
}
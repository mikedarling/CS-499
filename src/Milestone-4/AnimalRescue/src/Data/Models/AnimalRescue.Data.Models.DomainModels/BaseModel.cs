using System.ComponentModel.DataAnnotations;

namespace AnimalRescue.Data.Models.DomainModels
{
    public abstract class BaseModel
    {

        [Required]
        public long Id { get; set; }

    }
}

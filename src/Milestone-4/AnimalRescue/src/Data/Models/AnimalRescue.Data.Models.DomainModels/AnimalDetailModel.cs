using System.ComponentModel.DataAnnotations;

namespace AnimalRescue.Data.Models.DomainModels
{
    public class AnimalDetailModel : BaseModel
    {

        [Required]
        [Display(Name = "Animal ID:")]
        public string AnimalId { get; set; }

        [Required]
        [Display(Name = "Name:")]
        public string Name { get; set; }

        [Display(Name = "Animal Type:")]
        public long AnimalTypeId { get; set; }

        [Display(Name = "Breed:")]
        public long[] BreedIds { get; set; }

        [Display(Name = "Mix:")]
        public bool Mix { get; set; }

    }
}

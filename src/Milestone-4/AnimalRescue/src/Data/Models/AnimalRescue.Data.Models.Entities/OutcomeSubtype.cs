using System.ComponentModel.DataAnnotations;

namespace AnimalRescue.Data.Models.Entities
{
    public class OutcomeSubtype : BaseEntity
    {

        [Required]
        public OutcomeType OutcomeType { get; set; }

    }
}
using System.ComponentModel.DataAnnotations;

namespace AnimalRescue.Data.Models.Entities
{
    public abstract class BaseEntity
    {

        [Key]
        public long Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

    }
}

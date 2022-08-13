using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AnimalRescue.Data.Models.Entities
{
    public class Animal : BaseEntity
    {

        #region Constructor

        public Animal()
        {
            this.Breeds = new List<Breed>();
            this.Colors = new List<Color>();
        }

        #endregion

        [MaxLength(20)]
        public string AnimalId { get; set; }

        public virtual AnimalType AnimalType { get; set; }

        public virtual ICollection<Breed> Breeds { get; set; }

        public bool IsMixedBreed { get; set; }

        public virtual ICollection<Color> Colors { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfOutcome { get; set; }

        public virtual OutcomeSubtype OutcomeSubtype { get; set; }

        public virtual OutcomeType OutcomeType { get; set; }

        public virtual Sex Sex { get; set; }

        [Range(-180.0, 180.0)]
        public double Latitude { get; set; }

        [Range(-180.0, 180.0)]
        public double Longitude { get; set; }

    }
}

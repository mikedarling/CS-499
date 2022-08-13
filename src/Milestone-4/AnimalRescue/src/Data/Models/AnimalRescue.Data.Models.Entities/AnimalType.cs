using System.Collections.Generic;

namespace AnimalRescue.Data.Models.Entities
{
    public class AnimalType : BaseEntity
    {

        #region Constructor

        public AnimalType()
        {
            this.Breeds = new List<Breed>();
        }

        #endregion

        public ICollection<Breed> Breeds { get; set; }

    }
}
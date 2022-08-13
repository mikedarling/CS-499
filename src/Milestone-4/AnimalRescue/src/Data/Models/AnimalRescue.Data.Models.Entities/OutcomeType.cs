using System.Collections.Generic;

namespace AnimalRescue.Data.Models.Entities
{
    public class OutcomeType : BaseEntity
    {

        #region Constructor

        public OutcomeType()
        {
            this.OutcomeSubtypes = new List<OutcomeSubtype>();
        }

        #endregion

        public virtual ICollection<OutcomeSubtype> OutcomeSubtypes { get; set; }

    }
}
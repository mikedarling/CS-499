using System;

namespace AnimalRescue.Data.Models.Entities
{
    public class Animal : BaseEntity
    {

        public string AgeUponOutcome { get; set; }

        public string AnimalId { get; set; }

        public string AnimalType { get; set; }

        public string Breed { get; set; }

        public string Color { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? Datetime { get; set; }

        public DateTime? Monthyear { get; set; }

        public string Name { get; set; }

        public string OutcomeSubtype { get; set; }

        public string OutcomeType { get; set; }

        public string SexUponOutcome { get; set; }

        public double LocationLat { get; set; }

        public double LocationLong { get; set; }

        public string AgeUponOutcomeInWeeks { get; set; }

    }
}

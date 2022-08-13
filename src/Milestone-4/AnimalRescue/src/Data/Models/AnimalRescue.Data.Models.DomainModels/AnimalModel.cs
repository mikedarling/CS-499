using System;

namespace AnimalRescue.Data.Models.DomainModels
{
    public class AnimalModel : BaseModel
    {

        public string AnimalId { get; set; }

        public string Name { get; set; }

        public string AnimalType { get; set; }

        public string Breed { get; set; }

        public string Color { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime? DateOfOutcome { get; set; }

        public string OutcomeSubtype { get; set; }

        public string OutcomeType { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string SexUponOutcome { get; set; }

        public string AgeUponOutcome { get; set; }

        public string AgeUponOutcomeInWeeks { get; set; }

    }
}

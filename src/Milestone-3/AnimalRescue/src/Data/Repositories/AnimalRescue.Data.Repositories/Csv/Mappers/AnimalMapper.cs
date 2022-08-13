using AnimalRescue.Data.Models.Entities;
using CsvHelper.Configuration;

namespace AnimalRescue.Data.Repositories.FlatFile.Csv.Mappers
{

    /// <summary>
    /// Utility class from the CSVHelper library that maps the CSV "Columns" to a C# class's property.
    /// </summary>
    public class AnimalMapper : ClassMap<Animal>
    {

        #region Constructors

        public AnimalMapper()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.AgeUponOutcome).Name("age_upon_outcome");
            Map(m => m.AnimalId).Name("animal_id");
            Map(m => m.AnimalType).Name("animal_type");
            Map(m => m.Breed).Name("breed");
            Map(m => m.Color).Name("color");
            Map(m => m.DateOfBirth).Name("date_of_birth");
            Map(m => m.Datetime).Name("datetime");
            Map(m => m.Monthyear).Name("monthyear");
            Map(m => m.Name).Name("name");
            Map(m => m.OutcomeSubtype).Name("outcome_subtype");
            Map(m => m.OutcomeType).Name("outcome_type");
            Map(m => m.SexUponOutcome).Name("sex_upon_outcome");
            Map(m => m.LocationLat).Name("location_lat");
            Map(m => m.LocationLong).Name("location_long");
            Map(m => m.AgeUponOutcomeInWeeks).Name("age_upon_outcome_in_weeks");
        }

        #endregion

    }
}

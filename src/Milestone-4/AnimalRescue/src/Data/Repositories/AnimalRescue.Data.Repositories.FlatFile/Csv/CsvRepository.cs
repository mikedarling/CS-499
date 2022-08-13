using AnimalRescue.Data.Repositories.FlatFile.Csv.Mappers;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AnimalRescue.Data.Repositories.FlatFile.Csv
{
    public class CsvRepository : IDataRepository
    {

        #region Constructors

        public CsvRepository(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            string appDataPath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            string filePath = Path.Combine(appDataPath, fileName);
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException(nameof(fileName));
            }

            this._filePath = filePath;
        }

        #endregion

        #region Local Variables

        private readonly string _filePath;

        #endregion

        #region Methods

        public IQueryable<T> GetRecords<T>()
        {
            List<T> models = null;

            using (var reader = new StreamReader(this._filePath))
            {
                using (var parser = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    parser.Context.RegisterClassMap<AnimalMapper>();

                    var records = parser.GetRecords<T>();
                    if (records == null || !records.Any())
                    {
                        return null;
                    }

                    models = records
                        .ToList();
                }
            }

            return (models != null || models.Any()) ? models.AsQueryable<T>() : null;
        }

        #endregion

    }
}

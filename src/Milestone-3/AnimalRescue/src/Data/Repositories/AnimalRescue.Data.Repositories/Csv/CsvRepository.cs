using AnimalRescue.Data.Models.Entities;
using AnimalRescue.Data.Repositories.FlatFile.Csv.Mappers;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace AnimalRescue.Data.Repositories.Csv
{
    /// <summary>
    /// Repository class for CSV files. Only provides read access.
    /// </summary>
    public class CsvRepository : IReadableDataRepository
    {

        #region Constructors

        /// <summary>
        /// Instantiates a <see cref="CsvRepository"/> that reads the specified
        /// file. The file is expected to exist in the /App_Data/ folder.
        /// </summary>
        /// <param name="fileName">The name of the file to read and parse.</param>
        /// <exception cref="ArgumentNullException">The filename must be specified.</exception>
        /// <exception cref="FileNotFoundException">The file was not found in the /App_Data/ folder.</exception>
        public CsvRepository(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            string filePath = this.GetFilePath(fileName);
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

        /// <summary>
        /// Returns a single entity of the specified type with the provided ID from the CSV.
        /// </summary>
        /// <typeparam name="T">A class based on the <see cref="BaseEntity"/> abstract class.</typeparam>
        /// <param name="id">The unique ID of the entity.</param>
        /// <returns>The entity with the matching ID.</returns>
        public T Get<T>(long id) where T : BaseEntity
        {
            return this.GetAll<T>()
                .FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Gets all entities of the specified type matching the specified set of conditions.
        /// Each predicate is effectively "AND'ed" on to the previous.
        /// </summary>
        /// <typeparam name="T">A class based on the <see cref="BaseEntity"/> abstract class.</typeparam>
        /// <param name="predicates">An array of predicate expressions that act as WHERE clauses.</param>
        /// <returns>An IQueryable of all matching entities</returns>
        public IQueryable<T> GetMany<T>(Expression<Func<T, bool>>[] predicates)
        {
            // We need to tie the results back to since the connection to the repo will 
            // be disposed and unavailable once we leave scope.
            List<T> models = null;

            // Opens a disposable instance of the CSV reader.
            using (var reader = new StreamReader(_filePath))
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

            if (predicates == null || !predicates.Any())
            {
                return models != null || models.Any() ? models.AsQueryable() : null;

            }

            var queryable = models.AsQueryable();

            foreach (var predicate in predicates)
            {
                queryable = queryable.Where(predicate);
            }

            return queryable;
        }

        /// <summary>
        /// Gets all entities of the specified type from the repository.
        /// </summary>
        /// <typeparam name="T">A class based on the <see cref="BaseEntity"/> abstract class.</typeparam>
        /// <returns>An IQueryable of all entities</returns>
        public IQueryable<T> GetAll<T>()
        {
            return this.GetMany<T>(null);
        }

        #endregion

        #region Helpers

        private string GetFilePath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            string appDataPath = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            return Path.Combine(appDataPath, fileName);
        }

        #endregion

    }
}

using AnimalRescue.Data.Repositories.DataRepos.Csv;
using AnimalRescue.Data.Repositories.DataRepos.Ef;
using AnimalRescue.Data.Repositories.EntityFramework;
using System;
using System.Configuration;

namespace AnimalRescue.Data.Repositories.DataRepos
{

    /// <summary>
    /// Factory class that returns the requested
    /// Repository type
    /// </summary>
    public class RepositoryFactory
    {

        #region Constructors

        // Constructor marked private to enforce Singleton pattern.
        private RepositoryFactory()
        {

        }

        #endregion

        #region Local Variables

        // The private Singleton instance. Lazy<T> implementation
        // defers objet creation until it is used.
        private static Lazy<RepositoryFactory> _instance;

        // Lock object to help enforce thread safety.
        private static object _lock = new object();

        #endregion

        #region Public Properties

        /// <summary>
        /// Public access to the encapsulated singleton. 
        /// </summary>
        public static RepositoryFactory Instance
        {
            get
            {
                // Object lock helps to ensure thread safety.
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Lazy<RepositoryFactory>(() => new RepositoryFactory(), true);
                    }
                    return _instance.Value;
                }
            }
        }

        #endregion

        #region Methods

        public IReadableDataRepository CreateReadable(string repoType)
        {
            switch (repoType)
            {
                case "csv":
                    string fileName = ConfigurationManager.AppSettings["CsvFilePath"];
                    return new CsvRepository(fileName);
                case "ef":
                    var db = new AnimalRescueContext();
                    if (db.Database.CreateIfNotExists())
                    {
                        // TODO :: Seed.
                    }
                    return new EfReadableDataRepository<AnimalRescueContext>(db);
                default:
                    throw new Exception("Specify a known repository type");
            }
        }

        public IWriteableDataRepository CreateWritable(string repoType)
        {
            switch (repoType)
            {
                case "ef":
                    var db = new AnimalRescueContext();
                    if (db.Database.CreateIfNotExists())
                    {
                        // TODO :: Seed.
                    }
                    return new EfWriteableDataRepository<AnimalRescueContext>(db);
                default:
                    throw new Exception("Specify a known repository type");
            }
        }

        #endregion

    }
}

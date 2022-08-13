using AnimalRescue.Data.Repositories.Csv;
using System;
using System.Configuration;

namespace AnimalRescue.Data.Repositories
{
    public class RepositoryFactory
    {

        #region Constructors

        private RepositoryFactory()
        {

        }

        #endregion

        #region Local Variables

        private static Lazy<RepositoryFactory> _instance;

        private static object _lock = new object();

        #endregion

        #region Public Properties

        public static RepositoryFactory Instance
        {
            get
            {
                lock(_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Lazy<RepositoryFactory>(() => new RepositoryFactory());
                    }
                    return _instance.Value;
                }
            }
        }

        #endregion

        #region Methods

        public IDataRepository Create(string repoType)
        {
            switch (repoType)
            {
                case "csv":
                    string fileName = ConfigurationManager.AppSettings["CsvFilePath"];
                    return new CsvRepository(fileName);
                default:
                    throw new Exception("Specify a known repository type");
            }
        }

        #endregion

    }
}

using AnimalRescue.Data.Models.DomainModels;
using AnimalRescue.Data.Models.Entities;
using AutoMapper;
using System;

namespace AnimalRescue.Services.AutoMapper
{
    public class MapperConfigurationFactory
    {

        #region Constructors

        private MapperConfigurationFactory()
        {
        }

        #endregion

        #region Local Variables

        private static Lazy<MapperConfigurationFactory> _factory;

        private static object _lock = new object();

        #endregion

        #region Public Properties

        public static MapperConfigurationFactory Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_factory == null)
                    {
                        _factory = new Lazy<MapperConfigurationFactory>(() => new MapperConfigurationFactory());
                    }
                    return _factory.Value;
                }
            }
        }

        #endregion

        #region Methods

        public MapperConfiguration Create()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Animal, AnimalModel>());
            return config;
        }

        #endregion

    }
}

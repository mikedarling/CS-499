using AnimalRescue.Data.Models.DomainModels;
using AnimalRescue.Data.Models.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Animal, AnimalModel>()
                    .ForMember(
                        dest => dest.AnimalType,
                        map => map.MapFrom(src => src.AnimalType != null ? src.AnimalType.Name : string.Empty)
                    )
                    .ForMember(
                        dest => dest.Breed,
                        map => map.MapFrom(src => GetBreedValue(src))
                    )
                    .ForMember(
                        dest => dest.OutcomeType,
                        map => map.MapFrom(src => src.OutcomeType != null ? src.OutcomeType.Name : string.Empty)
                    )
                    .ForMember(
                        dest => dest.OutcomeSubtype,
                        map => map.MapFrom(src => src.OutcomeSubtype != null ? src.OutcomeSubtype.Name : string.Empty)
                    )
                    .ForMember(
                        dest => dest.Color,
                        map => map.MapFrom(src => GetColorValue(src))
                    )
                    .ForMember(
                        dest => dest.SexUponOutcome,
                        map => map.MapFrom(src => src.Sex != null ? src.Sex.Name : string.Empty)
                    );

                cfg.CreateMap<Breed, BreedModel>()
                    .ForMember(
                        dest => dest.AnimalTypeId,
                        map => map.MapFrom(src => src.AnimalType != null ? src.AnimalType.Id : 0)
                    );

                cfg.CreateMap<AnimalType, AnimalTypeModel>();

                cfg.CreateMap<Animal, AnimalDetailModel>()
                    .ForMember(
                        dest => dest.BreedIds,
                        map => map.MapFrom(src => (src.Breeds != null && src.Breeds.Any()) ? src.Breeds.Select(x => x.Id).ToArray() : null)
                    )
                    .ForMember(
                        dest => dest.Mix,
                        map => map.MapFrom(src => src.IsMixedBreed)
                    );
            });
            return config;
        }

        #endregion

        #region Helpers

        private string GetBreedValue(Animal model)
        {
            if (model == null || model.Breeds == null || !model.Breeds.Any())
            {
                return string.Empty;
            }

            if (model.Breeds.Count == 1)
            {
                var breed = model.Breeds
                    .FirstOrDefault();
                if (breed == null)
                {
                    return string.Empty;
                }

                return !model.IsMixedBreed ? breed.Name : $"{breed.Name} Mix";
            }

            var breeds = model.Breeds
                .Where(x => x != null && !string.IsNullOrEmpty(x.Name));
            if (breeds == null || !breeds.Any())
            {
                return string.Empty;
            }

            IEnumerable<string> names = breeds
                .Select(x => x.Name);

            return string.Join("/", names);
        }

        private object GetColorValue(Animal model)
        {
            if (model == null || model.Colors == null || !model.Colors.Any())
            {
                return string.Empty;
            }

            var colors = model.Colors
                .Where(x => !string.IsNullOrEmpty(x.Name));

            if (colors == null || !colors.Any())
            {
                return string.Empty;
            }

            IEnumerable<string> names = colors
                .Select(x => x.Name);

            return string.Join("/", names);
        }

        #endregion

    }
}

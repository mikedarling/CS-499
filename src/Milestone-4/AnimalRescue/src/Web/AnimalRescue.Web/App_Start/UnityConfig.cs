using AnimalRescue.Caching.Managers;
using AnimalRescue.Caching.Providers;
using AnimalRescue.Data.Repositories.DataRepos;
using AnimalRescue.Security.Authentication;
using AnimalRescue.Services.Animals;
using AnimalRescue.Services.AnimalTypes;
using AnimalRescue.Services.AutoMapper;
using AnimalRescue.Services.Breeds;
using AutoMapper;
using Microsoft.Owin;
using System.Web;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace AnimalRescue.Web
{
    public static class UnityConfig
    {

        /// <summary>
        /// Sets up dependency injection container. When the registered types are defined as constructor
        /// arguments, the DI container will return an object of the registered type.
        /// </summary>
        public static void RegisterComponents()
        {
            var container = new UnityContainer();


            container.RegisterFactory<IReadableDataRepository>(c => RepositoryFactory.Instance.CreateReadable("ef"));
            container.RegisterFactory<IWriteableDataRepository>(c => RepositoryFactory.Instance.CreateWritable("ef"));

            container.RegisterFactory<IOwinContext>(c => HttpContext.Current.GetOwinContext());

            container.RegisterType<ICacheProvider, MemoryCacheProvider>();

            var cacheProvider = container.Resolve<ICacheProvider>();
            container.RegisterInstance(new AnimalCacheManager(cacheProvider));
            container.RegisterInstance(new AnimalTypeCacheManager(cacheProvider));
            container.RegisterInstance(new BreedCacheManager(cacheProvider));

            container.RegisterType<IAnimalService, AnimalService>();
            container.RegisterType<IAnimalTypeService, AnimalTypeService>();
            container.RegisterType<IBreedService, BreedService>();
            container.RegisterType<IUserService, UserService>();

            container.RegisterInstance<IMapper>(new Mapper(MapperConfigurationFactory.Instance.Create()));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
using AnimalRescue.Data.Repositories;
using AnimalRescue.Services.Animals;
using AnimalRescue.Services.AutoMapper;
using AutoMapper;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace AnimalRescue.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IAnimalService, AnimalService>();

            container.RegisterInstance(RepositoryFactory.Instance.Create("csv"));
            container.RegisterInstance<IMapper>(new Mapper(MapperConfigurationFactory.Instance.Create()));

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
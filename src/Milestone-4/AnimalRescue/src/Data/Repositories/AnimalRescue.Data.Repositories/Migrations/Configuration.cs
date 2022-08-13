namespace AnimalRescue.Data.Repositories.Migrations
{
    using AnimalRescue.Data.Models.Entities;
    using AnimalRescue.Data.Repositories.DataRepos;
    using AnimalRescue.Data.Repositories.EntityFramework;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AnimalRescueContext>
    {

        #region Constructor

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        #endregion

        #region Local Variables

        private readonly IReadableDataRepository _sourceRepository = RepositoryFactory.Instance.CreateReadable("csv");

        private const string ADMIN = "Administrator";

        private const string EDITOR = "Editor";

        private const string VIEWER = "Viewer";

        #endregion

        #region Methods

        protected override void Seed(AnimalRescueContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (context.Animals.FirstOrDefault() == null)
            {
                var sourceData = this._sourceRepository.GetAll<AnimalCsvRecord>();
                this.SeedAnimalTypesAndBreeds(context, sourceData);
                this.SeedOutcomeTypesAndSubtypes(context, sourceData);
                this.SeedColors(context, sourceData);
                this.SeedSex(context, sourceData);
                this.SeedAnimals(context, sourceData);
            }

            this.SeedRoles(context);
            this.SeedUsers(context);

        }

        #endregion

        #region Helpers

        private void SeedAnimalTypesAndBreeds(AnimalRescueContext context, IQueryable<AnimalCsvRecord> sourceData)
        {
            var animalTypeEntities = sourceData
                             .Select(x => x.AnimalType.Trim())
                             .Distinct()
                             .Select(x => new AnimalType()
                             {
                                 Name = x
                             })
                             .AsEnumerable();

            animalTypeEntities = context.AnimalTypes.AddRange(animalTypeEntities);
            context.SaveChanges();

            foreach (var animalTypeEntity in animalTypeEntities)
            {
                var animalType_BreedEntities = sourceData
                    .Where(x => x.AnimalType.Trim() == animalTypeEntity.Name)
                    .SelectMany(x => x.Breed.Split('/'))
                    .Select(x => x.Replace(" Mix", "").Trim())
                    .Distinct()
                    .Select(x => new Breed()
                    {
                        Name = x,
                        AnimalType = animalTypeEntity
                    })
                    .AsEnumerable();

                animalType_BreedEntities = context.Breeds.AddRange(animalType_BreedEntities);
                context.SaveChanges();

                //animalTypeEntity.Breeds = animalType_BreedEntities
                //    .ToList();
                //context.SaveChanges();
            }
        }

        private void SeedOutcomeTypesAndSubtypes(AnimalRescueContext context, IQueryable<AnimalCsvRecord> sourceData)
        {
            var outcomeTypeEntities = sourceData
                            .Select(x => x.OutcomeType.Trim())
                            .Distinct()
                            .Select(x => new OutcomeType()
                            {
                                Name = x
                            })
                            .AsEnumerable();

            outcomeTypeEntities = context.OutcomeTypes.AddRange(outcomeTypeEntities);

            foreach (var outcomeTypeEntity in outcomeTypeEntities)
            {
                var outcomeSubTypeEntities = sourceData
                    .Where(x => x.OutcomeType.Trim() == outcomeTypeEntity.Name)
                    .Select(x => x.OutcomeSubtype.Trim())
                    .Distinct()
                    .Select(x => new OutcomeSubtype()
                    {
                        Name = outcomeTypeEntity.Name,
                        OutcomeType = outcomeTypeEntity
                    })
                    .AsEnumerable();

                outcomeSubTypeEntities = context.OutcomeSubtypes.AddRange(outcomeSubTypeEntities);
                context.SaveChanges();

                //outcomeTypeEntity.OutcomeSubtypes = outcomeSubTypeEntities
                //    .ToList();
                //context.SaveChanges();
            }
        }

        private void SeedColors(AnimalRescueContext context, IQueryable<AnimalCsvRecord> sourceData)
        {
            var colorEntities = sourceData
            .SelectMany(x => x.Color.Split('/'))
            .Select(x => x.Trim())
            .Distinct()
            .Select(x => new Color()
            {
                Name = x
            })
            .AsEnumerable();

            context.Colors.AddRange(colorEntities);
        }
        
        private void SeedSex(AnimalRescueContext context, IQueryable<AnimalCsvRecord> sourceData)
        {
            var sexEntities = sourceData
            .Select(x => x.SexUponOutcome.Trim())
            .Distinct()
            .Select(x => new Sex()
            {
                Name = x
            })
            .AsEnumerable();

            context.Sexes.AddRange(sexEntities);
        }

        private void SeedAnimals(AnimalRescueContext context, IQueryable<AnimalCsvRecord> sourceData)
        {
            foreach (var record in sourceData)
            {
                Animal animalEntity = new Animal()
                {
                    Name = record.Name,
                    AnimalId = record.AnimalId,
                    Latitude = record.LocationLat,
                    Longitude = record.LocationLong,
                    DateOfBirth = record.DateOfBirth,
                    DateOfOutcome = record.Datetime
                };

                animalEntity.AnimalType = context.AnimalTypes
                    .FirstOrDefault(x => x.Name == record.AnimalType.Trim());

                animalEntity.IsMixedBreed = record.Breed.Contains(" Mix") || record.Breed.Split('/').Length > 1;

                animalEntity.Breeds = this.SetBreeds(context, record);

                animalEntity.OutcomeSubtype = context.OutcomeSubtypes
                                .FirstOrDefault(x => x.Name == record.OutcomeSubtype.Trim());

                animalEntity.OutcomeType = this.GetOutcomeType(context, record, animalEntity.OutcomeSubtype);

                animalEntity.Colors = this.GetColors(context, record);

                animalEntity.Sex = context.Sexes
                    .FirstOrDefault(x => x.Name == record.SexUponOutcome.Trim());

                context.Animals.Add(animalEntity);
                context.SaveChanges();
            }
        }

        private void SeedRoles(AnimalRescueContext context)
        {
            var roles = new string[] { ADMIN, EDITOR, VIEWER }
                .Select(x => new IdentityRole(x));
            context.Set<IdentityRole>().AddRange(roles);
            context.SaveChanges();
        }

        private void SeedUsers(AnimalRescueContext context)
        {
            Dictionary<string, string> users = new Dictionary<string, string>();
            users.Add("adminUser", ADMIN);
            users.Add("editorUser", EDITOR);
            users.Add("viewerUser", VIEWER);

            var userMgr = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            

            foreach (var user in users)
            {
                var roleId = context.Set<IdentityRole>()
                    .FirstOrDefault(x => x.Name == user.Value)
                    .Id;

                var entity = new ApplicationUser()
                {
                    UserName = user.Key,
                    PasswordHash = userMgr.PasswordHasher.HashPassword("Password4!"),
                    Email = $"{user.Key}@grazioso.com",
                    SecurityStamp = Guid.NewGuid().ToString()
                    
                };

                entity = context.Set<ApplicationUser>().Add(entity);
                context.SaveChanges();

                var userRole = new IdentityUserRole()
                {
                    UserId = entity.Id,
                    RoleId = roleId
                };

                context.Set<IdentityUserRole>().Add(userRole);
                context.SaveChanges();
            }
        }

        private ICollection<Color> GetColors(AnimalRescueContext context, AnimalCsvRecord record)
        {
            var recordColors = record.Color
                    .Split('/')
                    .Select(x => x.Trim());

            var colors = context.Colors
                .Where(x => recordColors.Contains(x.Name));

            return (colors != null && colors.Any()) ? colors.ToList() : null;
        }

        private OutcomeType GetOutcomeType(AnimalRescueContext context, AnimalCsvRecord record, OutcomeSubtype subType)
        {
            if (subType != null)
            {
                return subType.OutcomeType;
            }

            return context.OutcomeTypes
                .FirstOrDefault(x => x.Name == record.OutcomeType.Trim());
        }

        private ICollection<Breed> SetBreeds(AnimalRescueContext context, AnimalCsvRecord record)
        {
            var recordBreeds = record.Breed
                .Split('/')
                .Select(x => x.Replace(" Mix", "").Trim());

            var breeds = context.Breeds
                .Where(x => recordBreeds.Contains(x.Name));

            return (breeds != null && breeds.Any()) ? breeds.ToList() : null;
        }

        #endregion

    }
}

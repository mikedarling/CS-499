using AnimalRescue.Data.Models.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace AnimalRescue.Data.Repositories.EntityFramework
{

    /// <summary>
    /// The Entity Framework database implementation. Inherits the <see cref="IdentityDbContext" />
    /// which is an extension of the standard EF DbContext class that includes support for ASP.NET
    /// Membership.
    /// </summary>
    public class AnimalRescueContext : IdentityDbContext<ApplicationUser>
    {

        #region Constructors

        public AnimalRescueContext()
            : base("name=animalRescueContext")
        {
        }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // This sets up the cross (supports many-to-many cardinality) table between Animals and Breeds.
            modelBuilder.Entity<Animal>()
                .HasMany(x => x.Breeds)
                .WithMany()
                .Map(x => x.ToTable("Animal_to_Breed"));

            // This sets up the cross (supports many-to-many cardinality) table between Animals and Colors.
            modelBuilder.Entity<Animal>()
                .HasMany(x => x.Colors)
                .WithMany()
                .Map(x => x.ToTable("Animal_to_Color"));

            // Set up the keys to complete the Membership implementation. 
            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
        }

        #endregion

        #region Sets

        public DbSet<Animal> Animals { get; set; }

        public DbSet<AnimalType> AnimalTypes { get; set; }

        public DbSet<Breed> Breeds { get; set; }

        public DbSet<Color> Colors { get; set; }

        public DbSet<OutcomeType> OutcomeTypes { get; set; }

        public DbSet<OutcomeSubtype> OutcomeSubtypes { get; set; }

        public DbSet<Sex> Sexes { get; set; }

        #endregion

    }
}

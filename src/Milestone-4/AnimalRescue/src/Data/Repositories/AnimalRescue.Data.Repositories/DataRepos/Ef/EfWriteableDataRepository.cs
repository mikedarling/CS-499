using AnimalRescue.Data.Models.Entities;
using System.Data.Entity;
using System.Threading.Tasks;

namespace AnimalRescue.Data.Repositories.DataRepos.Ef
{

    /// <summary>
    /// Derived class that implements a writeable data repository backed by an EF database.
    /// Use of Generics at the class level (i.e. <typeparamref name="TContext"/>) allows any EF database to be used
    /// with any type of DBSet when combined with the \<T\> on the method signatures.
    /// </summary>
    /// <typeparam name="TContext">Generic EF Database definition.</typeparam>
    public class EfWriteableDataRepository<TContext> : IWriteableDataRepository
    {

        #region Constructors

        public EfWriteableDataRepository(DbContext db)
        {
            this._db = db;
        }

        #endregion

        #region Local Variables

        private DbContext _db;

        #endregion

        #region Methods

        public T Update<T>(T model) where T : BaseEntity 
        {
            var entity = this._db.Set<T>().Attach(model);
            var entry = this._db.Entry(entity);
            entry.State = EntityState.Modified;
            this._db.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync<T>(T model) where T : BaseEntity
        {
            var entity = this._db.Set<T>().Attach(model);
            var entry = this._db.Entry(entity);
            entry.State = EntityState.Modified;
            await this._db.SaveChangesAsync();
            return entity;
        }

        #endregion

    }
}

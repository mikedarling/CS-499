using AnimalRescue.Data.Repositories.DataRepos.Base;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AnimalRescue.Data.Repositories.DataRepos.Ef
{

    /// <summary>
    /// Derived class that implements a readable data repository backed by an EF database.
    /// Use of Generics at the class level (i.e. <typeparamref name="TContext"/>) allows any EF database to be used
    /// with any type of DBSet when combined with the \<T\> on the method signatures.
    /// </summary>
    /// <typeparam name="TContext">Generic EF Database definition.</typeparam>
    public class EfReadableDataRepository<TContext> : BaseReadableDataRepository, IReadableDataRepository where TContext : DbContext
    {

        #region Constructors

        public EfReadableDataRepository(DbContext db)
        {
            this._db = db;
        }

        #endregion

        #region Local Variables

        private DbContext _db;

        #endregion

        #region Methods

        #region Synchronous

        public override T Get<T>(long id)
        {
            return _db.Set<T>()
               .AsNoTracking()
               .FirstOrDefault(x => x.Id == id);
        }

        public override IQueryable<T> GetMany<T>(Expression<Func<T, bool>>[] predicates)
        {
            var models = _db.Set<T>()
                .AsNoTracking()
                .AsQueryable();

            if (predicates == null || !predicates.Any())
            {
                return models;
            }

            foreach (var predicate in predicates)
            {
                models = models
                    .Where(predicate);
            }

            return models;
        }

        #endregion

        #endregion

    }
}

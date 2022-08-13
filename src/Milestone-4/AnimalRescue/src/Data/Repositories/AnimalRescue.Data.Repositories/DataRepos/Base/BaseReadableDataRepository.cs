using AnimalRescue.Data.Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnimalRescue.Data.Repositories.DataRepos.Base
{

    /// <summary>
    /// Abstract base readable data repository class
    /// that provides basic synchronous and asynchronous
    /// data retrieval methods.
    /// </summary>
    public abstract class BaseReadableDataRepository : IReadableDataRepository
    { 

        #region Synchronous

        public abstract T Get<T>(long id) where T : BaseEntity;

        public virtual T Get<T>(Expression<Func<T, bool>>[] predicates) where T : BaseEntity
        {
            return this.GetMany<T>(predicates)
                .FirstOrDefault();
        }

        public virtual T Get<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            return this.GetMany<T>(new Expression<Func<T, bool>>[] { predicate })
                .FirstOrDefault();
        }

        public abstract IQueryable<T> GetMany<T>(Expression<Func<T, bool>>[] predicates) where T : BaseEntity;

        public virtual IQueryable<T> GetAll<T>() where T : BaseEntity
        {
            return this.GetMany<T>(null);
        }

        #endregion

        #region Async

        public virtual async Task<T> GetAsync<T>(long id) where T : BaseEntity
        {
            var task = Task.Run(() => this.Get<T>(id));
            return await task;
        }

        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>>[] predicates) where T : BaseEntity
        {
            var task = Task.Run(() => this.Get<T>(predicates));
            return await task;
        }

        public virtual async Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity
        {
            var task = Task.Run(() => this.Get<T>(predicate));
            return await task;
        }

        public virtual async Task<IQueryable<T>> GetManyAsync<T>(Expression<Func<T, bool>>[] predicates) where T : BaseEntity
        {
            var task = Task.Run(() => this.GetMany<T>(predicates));
            return await task;
        }

        public virtual async Task<IQueryable<T>> GetAllAsync<T>() where T : BaseEntity
        {
            var task = Task.Run(() => this.GetAll<T>());
            return await task;
        }

        #endregion

    }
}

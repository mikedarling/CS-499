using AnimalRescue.Data.Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AnimalRescue.Data.Repositories.DataRepos
{
    public interface IReadableDataRepository
    {

        #region Synchronous

        T Get<T>(long id) where T : BaseEntity;

        T Get<T>(Expression<Func<T, bool>>[] predicates) where T : BaseEntity;

        T Get<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity;

        IQueryable<T> GetMany<T>(Expression<Func<T, bool>>[] predicates) where T : BaseEntity;

        IQueryable<T> GetAll<T>() where T : BaseEntity;

        #endregion

        #region Async

        Task<T> GetAsync<T>(long id) where T : BaseEntity;

        Task<T> GetAsync<T>(Expression<Func<T, bool>>[] predicates) where T : BaseEntity;

        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : BaseEntity;

        Task<IQueryable<T>> GetManyAsync<T>(Expression<Func<T, bool>>[] predicates) where T : BaseEntity;

        Task<IQueryable<T>> GetAllAsync<T>() where T : BaseEntity;

        #endregion

    }
}

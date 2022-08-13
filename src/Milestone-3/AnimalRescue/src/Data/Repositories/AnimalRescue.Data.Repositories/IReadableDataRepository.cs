using AnimalRescue.Data.Models.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace AnimalRescue.Data.Repositories
{
    public interface IReadableDataRepository
    {

        T Get<T>(long id) where T : BaseEntity;

        IQueryable<T> GetMany<T>(Expression<Func<T, bool>>[] predicates);

        IQueryable<T> GetAll<T>();

    }
}

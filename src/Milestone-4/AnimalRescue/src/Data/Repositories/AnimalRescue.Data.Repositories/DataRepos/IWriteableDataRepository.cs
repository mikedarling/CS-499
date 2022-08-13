using AnimalRescue.Data.Models.Entities;
using System.Threading.Tasks;

namespace AnimalRescue.Data.Repositories.DataRepos
{
    public interface IWriteableDataRepository
    {

        #region Synchronous

        T Update<T>(T model) where T : BaseEntity;

        #endregion

        #region Async

        Task<T> UpdateAsync<T>(T model) where T : BaseEntity;

        #endregion

    }
}

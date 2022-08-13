using System.Linq;

namespace AnimalRescue.Data.Repositories
{
    public interface IDataRepository
    {
        IQueryable<T> GetRecords<T>();
    }
}

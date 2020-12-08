using System.Collections.Generic;
using System.Linq;

namespace Project.Core.Data
{
    public interface IRepository<T> where T : BaseEntity
    {
        T GetById(int Id);
        IEnumerable<T> GetAll();
        int Insert(T entity);
        int Insert(IEnumerable<T> entities);
        int Update(T entity);
        int Update(IEnumerable<T> entities);
        int Delete(T entity);
        int Delete(IEnumerable<T> entities);
        IQueryable<T> Table { get; }
        IQueryable<T> AsNoTracking { get; }
    }
}

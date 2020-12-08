using Project.Core;
using Project.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Test
{
    public class FakeRepository<T> : IRepository<T> where T : BaseEntity
    {
        public IQueryable<T> Table => default;

        public IQueryable<T> AsNoTracking =>  default;

        public int Delete(T entity)
        {
            return default;
        }

        public int Delete(IEnumerable<T> entities)
        {
            return default;
        }

        public IEnumerable<T> GetAll()
        {
            return default;
        }

        public T GetById(int Id)
        {
            return default;
        }
        public int Insert(T entity)
        {
            return default;
        }

        public int Insert(IEnumerable<T> entities)
        {
            return default;
        }

        public int Update(T entity)
        {
            return default;
        }

        public int Update(IEnumerable<T> entities)
        {
            return default;
        }
    }
}

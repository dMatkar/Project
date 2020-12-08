using Project.Core;
using Project.Core.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace Project.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly IDbContext _dbContext;
        private IDbSet<T> _entity;

        #endregion

        #region Constructor

        public EfRepository(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Properties

        protected virtual IDbSet<T> Entity
        {
            get => _entity ?? (_entity = _dbContext.Set<T>());
        }

        #endregion

        #region Methods

        public IQueryable<T> Table => Entity;

        public IQueryable<T> AsNoTracking => Entity.AsNoTracking();

        public int Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entity.Remove(entity);
                return _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public int Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                return entities.Select(entity => Delete(entities)).Sum();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public T GetById(int Id)
        {
            if (Id == 0)
                return default;

            return Entity.Where(x => x.Id == Id).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return Entity.ToList();
        }

        public int Insert(T entity)
        {
            if (entity is null)
                return default;

            Entity.Add(entity);
            return _dbContext.SaveChanges();
        }

        public int Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                return entities.Select(entity => Insert(entity)).Sum();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public int Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                return _dbContext.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        public int Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException("entities");

                return entities.Select(entity => Update(entity)).Sum();
            }
            catch (DbEntityValidationException dbEx)
            {
                throw new Exception(dbEx.Message, dbEx);
            }
        }

        #endregion
    }
}

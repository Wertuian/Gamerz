using Gamerz.Data.Context;
using Gamerz.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Data.Repository
{
    public class SqlRepository<T> : IRepository<T> where T : BaseEntity
    {

        private readonly GamerzContext _db;
        private readonly DbSet<T> _dbSet;
        public SqlRepository(GamerzContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }


        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            Delete(entity);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return predicate is not null ? _dbSet.Where(predicate) : _dbSet;
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Update(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            _dbSet.Update(entity);
            _db.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Gamerz.Data.Repository
{
    public interface  IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Delete(int id);
        void Update(T entity);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);

    }
}

using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repository.Abstract
{
    public interface IRepository<T> where T : BaseEntity

    {
        public int Insert(T input);
        public int Update(T input);
        public int Delete(T input);
        public int DeleteById(int id);
        public List<T>? GetAll(Expression<Func<T, bool>> predicate = null);
        public T? GetById(int id);

        public T? Get(Expression<Func<T, bool>> predicate = null);

        IQueryable<T> GetAllInclude(Expression<Func<T, bool>>? predicate,
           params Expression<Func<T, object>>[] include);
    }
}

using Blog.DAL.Repository.Abstract;
using Blog.Entities.DbContexts;
using Blog.Entities.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Repository.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        readonly AppDbContext _dbContext;
        readonly DbSet<T> _dbSet;

        public Repository()
        {
            _dbContext = new AppDbContext();
            _dbSet = _dbContext.Set<T>();
        }

        public virtual int Delete(T input)
        {
            throw new NotImplementedException();
        }

        public virtual int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual T? Get(Expression<Func<T, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public virtual List<T>? GetAll(Expression<Func<T, bool>> predicate = null)
        {
            throw new NotImplementedException();
        }

        public virtual IQueryable<T> GetAllInclude(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[] include)
        {
            throw new NotImplementedException();
        }

        public virtual T? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual int Insert(T input)
        {
            throw new NotImplementedException();
        }

        public virtual int Update(T input)
        {
            throw new NotImplementedException();
        }
    }
}

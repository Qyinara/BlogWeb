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
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual int Delete(T input)
        {
            _dbSet.Remove(input);
            return _dbContext.SaveChanges();
        }

        public virtual int DeleteById(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                return _dbContext.SaveChanges();
            }
            return 0;
        }

        public virtual T? Get(Expression<Func<T, bool>> predicate = null)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public virtual List<T>? GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return predicate == null ? _dbSet.ToList() : _dbSet.Where(predicate).ToList();
        }

        public virtual IQueryable<T> GetAllInclude(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = _dbSet;
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (include != null)
            {
                query = include.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            }

            return query;
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual int Insert(T input)
        {
            _dbSet.Add(input);
            return _dbContext.SaveChanges();
        }

        public virtual int Update(T input)
        {
            _dbSet.Update(input);
            return _dbContext.SaveChanges();
        }
    }

}

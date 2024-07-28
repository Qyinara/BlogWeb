using Blog.BL.Managers.Abstract;
using Blog.DAL.Repository.Concrete;
using Blog.Entities.DbContexts;
using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.BL.Managers.Concrete
{
    public class ManagerBase<T> : Repository<T>, IManager<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public ManagerBase(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override int Insert(T entity)
        {
            return base.Insert(entity);
        }

        public override int Update(T entity)
        {
            return base.Update(entity);
        }

        public override int Delete(T entity)
        {
            return base.Delete(entity);
        }

        public override T GetById(int id)
        {
            return base.GetById(id);
        }

        public override List<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            return base.GetAll(predicate);
        }

        public override IQueryable<T> GetAllInclude(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[] include)
        {
            return base.GetAllInclude(predicate, include);
        }

        public Task<T> ValidateUserAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

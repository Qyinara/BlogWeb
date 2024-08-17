using Blog.BL.Managers.Abstract;
using Blog.DAL.Repository.Concrete;
using Blog.Entities.DbContexts;
using Blog.Entities.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
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

        public override int DeleteById(int id)
        {
            return base.DeleteById(id);
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
            // Bu metod, UserManager gibi özel sınıflar tarafından override edilmelidir.
            throw new NotImplementedException();
        }
    }
}

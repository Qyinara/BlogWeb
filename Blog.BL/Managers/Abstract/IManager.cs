using Blog.DAL.Repository.Abstract;
using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BL.Managers.Abstract
{
    public interface IManager<T> where T : BaseEntity
    {
        int Insert(T input);
        int Update(T input);
        int Delete(T input);
        int DeleteById(int id);
        T GetById(int id);
        List<T> GetAll(Expression<Func<T, bool>> predicate = null);
        T Get(Expression<Func<T, bool>> predicate = null);
        IQueryable<T> GetAllInclude(Expression<Func<T, bool>>? predicate, params Expression<Func<T, object>>[] include);


        Task<T> ValidateUserAsync(string username, string password);
        Task AddAsync(T entity);
    }
}

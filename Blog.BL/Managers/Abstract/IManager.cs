using Blog.DAL.Repository.Abstract;
using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BL.Managers.Abstract
{
    public interface IManager<T> : IRepository<T> where T : BaseEntity
    {
    }
}

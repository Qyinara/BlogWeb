using Blog.BL.Managers.Abstract;
using Blog.Entities.DbContexts;
using Blog.Entities.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BL.Managers.Concrete
{
    public class RoleManager : ManagerBase<Role>, IManager<Role>
    {
        public RoleManager(AppDbContext context) : base(context)
        {
        }

        public new void Add(Role entity)
        {
            base.Insert(entity);
        }

        public new void Update(Role entity)
        {
            base.Update(entity);
        }

        public new void Delete(int id)
        {
            var entity = base.GetById(id);
            if (entity != null)
            {
                base.Delete(entity);
            }
        }

        public new Role GetById(int id)
        {
            return base.GetById(id);
        }

        public new List<Role> GetAll()
        {
            return base.GetAll();
        }
    }
}

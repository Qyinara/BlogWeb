using Blog.BL.Managers.Abstract;
using Blog.Entities.DbContexts;
using Blog.Entities.Models.Concrete;
using System.Collections.Generic;

namespace Blog.BL.Managers.Concrete
{
    public class ActivityManager : ManagerBase<Activity>, IManager<Activity>
    {
        public ActivityManager(AppDbContext context) : base(context)
        {
        }

        public void Add(Activity entity)
        {
            base.Insert(entity);
        }

        public void Update(Activity entity)
        {
            base.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = base.GetById(id);
            if (entity != null)
            {
                base.Delete(entity);
            }
        }

        public Activity GetById(int id)
        {
            return base.GetById(id);
        }

        public List<Activity> GetAll()
        {
            return base.GetAll();
        }
    }
}
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
    public class CommentManager : ManagerBase<Comment>, IManager<Comment>
    {
        public CommentManager(AppDbContext context) : base(context)
        {
        }

        public new void Add(Comment entity)
        {
            base.Insert(entity);
        }

        public new void Update(Comment entity)
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

        public new Comment GetById(int id)
        {
            return base.GetById(id);
        }

        public new List<Comment> GetAll()
        {
            return base.GetAll();
        }
    }
}

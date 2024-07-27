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
    public class PostLikeManager : ManagerBase<PostLike>, IManager<PostLike>
    {
        public PostLikeManager(AppDbContext context) : base(context)
        {
        }

        public new void Add(PostLike entity)
        {
            base.Insert(entity);
        }

        public new void Update(PostLike entity)
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

        public new PostLike GetById(int id)
        {
            return base.GetById(id);
        }

        public new List<PostLike> GetAll()
        {
            return base.GetAll();
        }
    }
}

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
    public class PostManager : ManagerBase<Post>, IManager<Post>
    {
        public PostManager(AppDbContext context) : base(context)
        {
        }

        public new void Add(Post entity)
        {
            base.Insert(entity);
        }

        public new void Update(Post entity)
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

        public new Post GetById(int id)
        {
            return base.GetById(id);
        }
        public new async Task AddAsync(Post entity)
        {
            await base.AddAsync(entity); // Asenkron olarak veriyi ekliyoruz
        }

        public new List<Post> GetAll()
        {
            return base.GetAll();
        }
    }
}

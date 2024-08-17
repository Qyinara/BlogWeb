using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Models.Concrete
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>(); // Eklenmesi gereken ilişki

    }
}

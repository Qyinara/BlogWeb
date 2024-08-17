using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Models.Concrete
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string? ProfilePhotoUrl { get; set; }



        public int RoleId { get; set; }
        public Role? Rolee { get; set; }

        public ICollection<Post> Posts { get; set; } = new List<Post>(); // Eklenmesi gereken ilişki
    }
}

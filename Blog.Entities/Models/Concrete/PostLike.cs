using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Models.Concrete
{
    public class PostLike : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } 
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}

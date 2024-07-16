using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Models.Concrete
{
    public class CommentLike : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } 
        public int CommentId { get; set; }
        public Comment Comment { get; set; }
    }
}

using Blog.Entities.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities.Models.Concrete
{
    public class Comment : BaseEntity
    {

        public int PostId { get; set; }
        public Post Post { get; set; } 
        public int AuthorId { get; set; }
        public User Author { get; set; } 
        public string Content { get; set; }
        public string CommentImageURL { get; set; }



        public ICollection<CommentLike> CommentLikes { get; set; }
    }

}

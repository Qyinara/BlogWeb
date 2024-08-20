using Blog.Entities.Models.Concrete;
using X.PagedList;

namespace BlogWeb.MVCUI.Models
{
    public class PostDetailsViewModel
    {
        public Post Post { get; set; }
        public IPagedList<Comment> PagedComments { get; set; } 
        public int TotalLikes { get; set; }
        public bool HasLiked { get; set; }
        public IEnumerable<CommentLike> CommentLikes { get; set; }
    }
}

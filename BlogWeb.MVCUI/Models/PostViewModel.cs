using Blog.Entities.Models.Concrete;

namespace BlogWeb.MVCUI.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public string PostImageURL { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();
    }
}

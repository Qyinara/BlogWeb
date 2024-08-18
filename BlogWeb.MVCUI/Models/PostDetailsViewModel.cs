using Blog.Entities.Models.Concrete;

public class PostDetailsViewModel
{
    public Post Post { get; set; }
    public List<Comment> Comments { get; set; }
    public int TotalLikes { get; set; }
}

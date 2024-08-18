namespace Blog.Api.Models
{
    public class CommentDto
    {
        public int PostId { get; set; }
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public string CommentImageURL { get; set; }
    }

}

using Blog.Entities.Models.Concrete;
using X.PagedList;

public class ProfileViewModel
{
    public User User { get; set; }
    public IPagedList<Activity> Activities { get; set; }
}

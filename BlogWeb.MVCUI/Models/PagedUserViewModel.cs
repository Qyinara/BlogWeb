using System.Collections.Generic;
using X.PagedList;

namespace BlogWeb.MVCUI.Models
{
    public class PagedUserViewModel
    {
        public IPagedList<UserViewModel> Users { get; set; }
    }
}

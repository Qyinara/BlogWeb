using System.ComponentModel.DataAnnotations;

namespace BlogWeb.MVCUI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; } 
        public string Mail { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Role { get; set; } 
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }

}

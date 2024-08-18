namespace BlogWeb.MVCUI.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }  // Bu alan güncelleniyor
        public string Mail { get; set; }
        public string ProfilePhotoUrl { get; set; }
        public string Role { get; set; }  // Admin veya User olacak
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }

}

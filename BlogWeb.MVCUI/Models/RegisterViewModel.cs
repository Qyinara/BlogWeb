namespace BlogWeb.MVCUI.Models
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public IFormFile? ProfilePhoto { get; set; } // Dosya yükleme işlemi için
    }



}

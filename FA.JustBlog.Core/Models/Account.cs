using System.ComponentModel.DataAnnotations;

namespace FA.JustBlog.Core.Models
{

    public class Account
    {
        [Required(ErrorMessage ="Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

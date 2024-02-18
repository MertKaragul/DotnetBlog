using System.ComponentModel.DataAnnotations;

namespace NLayerBlog.Models.UserViewModel {
    public class LoginViewModel {

        [EmailAddress]
        [Required]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }

        public bool IsRemember { get; set; }
    }
}

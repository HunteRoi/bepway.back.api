using System.ComponentModel.DataAnnotations;

namespace DTO {
    public class LoginModel {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
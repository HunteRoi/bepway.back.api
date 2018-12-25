using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class NewLoginModel : LoginModel
    {
        [Required]
        public string NewPassword {get; set;}

    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class SigninModel
    {
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public DateTime Birthdate { get; set; }
        public string Roles => null;
        public bool IsEnabled { get; set; }
        public byte[] RowVersion { get; set; }
    }
}
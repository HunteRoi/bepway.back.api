using System;
using System.ComponentModel;
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

        [DefaultValue(null)]
        public string Roles {get; set; }

        [DefaultValue(true)]
        public bool IsEnabled { get; set; }
        
        public byte[] RowVersion { get; set; }
    }
}
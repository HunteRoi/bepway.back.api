using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class SignupModel
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

        [DefaultValue("Guest")]
        public string Roles { get; set; }

        [DefaultValue(true)]
        public bool IsEnabled { get; set; }

        [StringLength(1000)]
        [DefaultValue(null)]
        public string TodoList { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
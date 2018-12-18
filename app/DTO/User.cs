using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime Birthdate { get; set; }

        [DefaultValue(null)]
        public string Roles { get; set; }

        [DefaultValue(true)]
        public bool IsEnabled { get; set; }

        [StringLength(1000)]
        public string TodoList { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class User
    {
        [Required]
        public decimal Id { get; set; }
        [Required]
        public string Login { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public string Roles { get; set; }
        public bool IsEnabled { get; set; }
        [StringLength(1000)]
        public string TodoList { get; set; }
        public byte[] RowVersion { get; set; }
        public DTO.User Creator { get; set; }
    }
}
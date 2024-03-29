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

        [DefaultValue("Guest")]
        public string Roles { get; set; }

        [StringLength(1000)]
        [DefaultValue(null)]
        public string TodoList { get; set; }
        
        public int? CreatorId { get; set; }
        
        [DefaultValue(false)]
        public bool IsEnabled { get; set; }
        
        public byte[] RowVersion { get; set; }
    }
}
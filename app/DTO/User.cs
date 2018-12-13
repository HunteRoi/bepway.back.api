using System;

namespace DTO
{
    public class User
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEnabled { get; set; }
        public string TodoList { get; set; }
        public DTO.User Creator { get; set; }
    }
}
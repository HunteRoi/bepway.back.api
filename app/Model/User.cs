using System;
using System.Collections.Generic;

namespace Model
{
    public partial class User
    {
        public User()
        {
            Company = new HashSet<Company>();
            InverseCreatorNavigation = new HashSet<User>();
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEnabled { get; set; }
        public string TodoList { get; set; }
        public string Creator { get; set; }

        public virtual User CreatorNavigation { get; set; }
        public virtual ICollection<Company> Company { get; set; }
        public virtual ICollection<User> InverseCreatorNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Model
{
    public class User
    {
        public User()
        {
            Audit = new HashSet<Audit>();
            Creation = new HashSet<Creation>();
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
        public byte[] RowVersion { get; set; }

        public virtual User CreatorNavigation { get; set; }
        public virtual ICollection<Audit> Audit { get; set; }
        public virtual ICollection<Creation> Creation { get; set; }
        public virtual ICollection<User> InverseCreatorNavigation { get; set; }
    }
}

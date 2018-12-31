using System;
using System.Collections.Generic;

namespace Model {
    public partial class User {
        public User () {
            Company = new HashSet<Company> ();
            InverseCreator = new HashSet<User> ();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Roles { get; set; }
        public bool IsEnabled { get; set; }
        public string TodoList { get; set; }
        public int? CreatorId { get; set; }
        public byte[] RowVersion { get; set; }

        public virtual User Creator { get; set; }
        public virtual ICollection<Company> Company { get; set; }
        public virtual ICollection<User> InverseCreator { get; set; }
    }
}
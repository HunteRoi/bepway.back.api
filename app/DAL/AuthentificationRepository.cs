// soon to be deleted

using System.Collections.Generic;
using Model;

namespace DAL
{
    public class AuthentificationRepository
    {
        private User[] _users = new User[]{
            new User() { Login="John Doe", Password="123" },
            new User() { Login="Jane Doe", Password="456" }
        };

        public IEnumerable<User> GetUsers() {
            return _users;
        }
    }
}
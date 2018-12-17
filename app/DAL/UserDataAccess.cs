using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class UserDataAccess : DataAccess<User>
    {
        public UserDataAccess (DbContext context)
        {
            Context = context;
        }

        // public override async Task<IEnumerable<User>> GetAllAsync (int? pageIndex = 0, int? pageSize = 5, string userName = null) {}
        // public override async Task<User> FindByIdAsync (int id) {}
        // public async Task<User> FindByLoginAsync (string login) {}
        // public override async Task<User> AddAsync (User data) {}
        // public override async Task<User> DeleteAsync (int id) {}
        // public override async Task<User> EditAsync (User data) {}
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class UserDataAccess : DataAccess<User>
    {
        public UserDataAccess (BepwayContext context)
        {
            Context = context;
        }

        public override async Task<IEnumerable<User>> GetAllAsync (int? pageIndex = 0, int? pageSize = 5, string userName = null)
        {
            return (await Context.User
                .Where(user => userName == null || user.Login.Contains(userName))
                .OrderBy(user => user.Id)
                .TakePage(pageIndex, pageSize)
                .ToArrayAsync());
        }
        public override async Task<User> FindByIdAsync (int id) 
        {
            return (await Context.User.FindAsync(id));
        }
        public async Task<User> FindByLoginAsync (string login) 
        {
            return (await Context.User.FirstOrDefaultAsync(user => user.Login.Contains(login)));
        }
        public override async Task AddAsync (User data) 
        {
            Context.User.Add(data);
            await Context.SaveChangesAsync();
        }
        // public override async Task<User> EditAsync (User data)
        // {

        // }
        protected override async Task DeleteAsync (User data) 
        {
            Context.User.Remove(data);
            await Context.SaveChangesAsync();
        }

        public override async Task<User> DeleteByIdAsync(int id)
        {
            User data = await FindByIdAsync(id);
            await DeleteAsync(data);
            return data;
        }        
    }
}
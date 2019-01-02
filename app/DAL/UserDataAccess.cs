using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class UserDataAccess : DataAccess<User>
    {
        public UserDataAccess(BepwayContext context)
        {
            Context = context;
        }

        public override int GetTotalCount(String userName = null, int? creatorId = null)
        {
            return Context.User
                .Where(u => 
                    (creatorId == null || u.CreatorId == creatorId) 
                    && 
                    (userName == null || u.Login.ToLower().Contains(userName.ToLower()))
                )
                .Count();
        }

        public override async Task<IEnumerable<User>> GetAllAsync(int? pageIndex = Constants.Page.Index, int? pageSize = Constants.Page.Size, String userName = null)
        {
            return await Context.User
                .Where(user => userName == null || user.Login.ToLower().Contains(userName.ToLower()))
                .OrderBy(user => user.Id)
                .TakePage(pageIndex.Value, pageSize.Value)
                .ToArrayAsync();
        }

        public override async Task<User> FindByIdAsync(int id)
        {
            return await Context.User.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> FindByLoginAsync(string login)
        {
            return await Context.User.FirstOrDefaultAsync(user => user.Login.ToLower().Contains(login.ToLower()));
        }

        public override async Task<User> AddAsync(User data)
        {
            Context.User.Add(data);
            await Context.SaveChangesAsync();
            return data;
        }

        public async Task<User> EditAsync(User data)
        {
            if (Context.Entry(data).State == EntityState.Detached)
            {
                Context.Attach(data).State = EntityState.Modified;
            }
            Context.Entry(data).OriginalValues["RowVersion"] = data.RowVersion;
            await Context.SaveChangesAsync();
            return data;
        }
        
        public override async Task DeleteAsync(User data)
        {
            Context.User.Remove(data);
            await Context.SaveChangesAsync();
        }
    }
}
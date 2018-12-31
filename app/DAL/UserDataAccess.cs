using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL {
    public class UserDataAccess : DataAccess<User> {
        public UserDataAccess (BepwayContext context) {
            Context = context;
        }

        public override async Task<IEnumerable<User>> GetAllAsync (int? pageIndex = 0, int? pageSize = 5, String userName = null) {
            IEnumerable<User> users = await Context.User
                .Where (user => userName == null || user.Login.Contains (userName))
                .OrderBy (user => user.Id)
                .TakePage (pageIndex.Value, pageSize.Value)
                .ToArrayAsync ();
            return users;
        }

        public override async Task<User> FindByIdAsync (int id) {
            User entity = await Context.User.FindAsync (id);
            return entity;
        }
        public async Task<User> FindByLoginAsync (string login) {
            User entity = await Context.User.FirstOrDefaultAsync (user => user.Login.Contains (login));
            return entity;
        }
        public override async Task<User> AddAsync (User data) {
            Context.User.Add (data);
            await Context.SaveChangesAsync ();
            User entity = await FindByLoginAsync (data.Login);
            return entity;
        }
        public async Task<User> EditAsync (User data) {
            if (Context.Entry (data).State == EntityState.Detached) {
                Context.Attach (data).State = EntityState.Modified;
            }
            Context.Entry (data).OriginalValues["RowVersion"] = data.RowVersion;
            await Context.SaveChangesAsync ();
            return data;
        }
        public override async Task DeleteAsync (User data) {
            Context.User.Remove (data);
            await Context.SaveChangesAsync ();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Model;
using Newtonsoft.Json.Linq;

namespace DAL
{
    public class ZoningDataAccess : DataAccess<Zoning>
    {
        public ZoningDataAccess(BepwayContext context)
        {
            Context = context;
        }

        private IIncludableQueryable<Zoning, Coordinates> ZoningQueryBase()
        {
            return Context.Zoning
                .Include(zoning => zoning.Coordinates);
        }

        public override int GetTotalCount(String zoningName = null, int? id = null)
        {
            return ZoningQueryBase().Where(z => zoningName == null || z.Name.ToLower().Contains(zoningName.ToLower())).Count();
        }

        public override async Task<IEnumerable<Zoning>> GetAllAsync(int? pageIndex = Constants.Page.Index, int? pageSize = Constants.Page.Size, String zoningName = null)
        {
            return await ZoningQueryBase()
                .Where(zoning => zoningName == null || zoning.Name.ToLower().Contains(zoningName.ToLower()))
                .OrderBy(zoning => zoning.Id)
                .TakePage(pageIndex.Value, pageSize.Value)
                .ToArrayAsync();
        }

        public override Task<Zoning> FindByIdAsync(int id)
        {
            return ZoningQueryBase().FirstOrDefaultAsync(z => z.Id == id);
        }

        private IEnumerable<Zoning> FindByName(string name)
        {
            return ZoningQueryBase().Where(c => c.Name.ToLower().Contains(name.ToLower()));
        }

        public override async Task<Zoning> AddAsync(Zoning data)
        {
            Context.Zoning.Add(data);
            await Context.SaveChangesAsync();
            return data;
        }
        
        public async Task<Zoning> EditAsync(Zoning data)
        {
            if (Context.Entry(data).State == EntityState.Detached)
            {
                Context.Attach(data).State = EntityState.Modified;
            }
            await Context.SaveChangesAsync();
            return data;
        }
        public override async Task DeleteAsync(Zoning data)
        {
            Context.Zoning.Remove(data);
            await Context.SaveChangesAsync();
        }
    }
}
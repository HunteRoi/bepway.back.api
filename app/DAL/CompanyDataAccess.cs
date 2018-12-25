using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Model;

using System.Globalization;
using System.Threading;
using System.Linq;

namespace DAL
{
    public class CompanyDataAccess : DataAccess<Company>
    {
        public CompanyDataAccess (BepwayContext context)
        {
            Context = context;
        }

        private Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Company, ActivitySector> CompanyQueryBase()
        {
            return Context.Company.Include(company => company.ActivitySector);
        }

        public override async Task<IEnumerable<Company>> GetAllAsync (int? pageIndex = 0, int? pageSize = 15, String companyName = null)
        {
            IEnumerable<Company> companies = await CompanyQueryBase()
                .Where(company => companyName == null || company.Name.Contains(companyName))
                .OrderBy(company => company.Id)
                .TakePage(pageIndex.Value, pageSize.Value)
                .ToArrayAsync();
            return companies;
        }

        public override async Task<Company> FindByIdAsync (int id) 
        {
            Company entity = await Context.Company.FindAsync(id);
            return entity;
        }
        public IEnumerable<Company> FindByName (string name) 
        {
            IEnumerable<Company> companies = CompanyQueryBase().Where(c => c.Name.Contains(name));
            return companies;
        }
        public async Task<Company> FindByAddressAsync (string address)
        {
            Company entity = await CompanyQueryBase().FirstOrDefaultAsync(company => company.Address.Contains(address));
            return entity;
        }
        public override async Task<Company> AddAsync (Company data) 
        {
            Context.Company.Add(data);
            await Context.SaveChangesAsync();
            Company entity = FindByName(data.Name).FirstOrDefault();
            return entity;
        }
        public async Task<Company> EditAsync (Company data)
        {
            if (Context.Entry(data).State == EntityState.Detached)
            {
                Context.Attach(data).State = EntityState.Modified;
            }
            await Context.SaveChangesAsync();
            return data;
        }
        public override async Task DeleteAsync (Company data) 
        {
            Context.Company.Remove(data);
            await Context.SaveChangesAsync();
        }
    }
}
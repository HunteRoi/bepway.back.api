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
    public class CompanyDataAccess : DataAccess<Company>
    {
        public CompanyDataAccess(BepwayContext context)
        {
            Context = context;
        }

        private IIncludableQueryable<Company, ActivitySector> PrivateCompanyQueryBase()
        {
            return Context.Company.Include(company => company.ActivitySector);
        }

        private IIncludableQueryable<Company, Coordinates> CompanyQueryBase()
        {
            return PrivateCompanyQueryBase().Include(company => company.Coordinates);
        }

        public override int GetTotalCount(String companyName = null)
        {
            return CompanyQueryBase().Where(c => companyName == null || c.Name.ToLower().Contains(companyName.ToLower())).Count();
        }

        public override async Task<IEnumerable<Company>> GetAllAsync(int? pageIndex = Constants.Page.Index, int? pageSize = Constants.Page.Size, String companyName = null)
        {
            return await CompanyQueryBase()
                .Where(company => companyName == null || company.Name.ToLower().Contains(companyName.ToLower()))
                .OrderBy(company => company.Id)
                .TakePage(pageIndex.Value, pageSize.Value)
                .ToArrayAsync();
        }

        public override async Task<Company> FindByIdAsync(int id)
        {
            return await CompanyQueryBase().FirstOrDefaultAsync(c => c.Id == id);
        }

        private IEnumerable<Company> FindByName(string name)
        {
            return CompanyQueryBase().Where(c => c.Name.ToLower().Contains(name.ToLower()));
        }

        public async Task<Company> FindByAddressAsync(string address)
        {
            return await CompanyQueryBase().FirstOrDefaultAsync(company => company.Address.ToLower().Contains(address.ToLower()));
        }

        public override async Task<Company> AddAsync(Company data)
        {
            Context.Company.Add(data);
            await Context.SaveChangesAsync();
            return data;
        }
        
        public async Task<Company> EditAsync(Company data)
        {
            if (Context.Entry(data).State == EntityState.Detached)
            {
                Context.Attach(data).State = EntityState.Modified;
            }
            Context.Entry(data).OriginalValues["RowVersion"] = data.RowVersion;
            await Context.SaveChangesAsync();
            return data;
        }
        public override async Task DeleteAsync(Company data)
        {
            Context.Company.Remove(data);
            await Context.SaveChangesAsync();
        }
    }
}
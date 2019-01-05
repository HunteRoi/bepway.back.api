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

        private IIncludableQueryable<Company, Zoning> CompanyQueryBase()
        {
            return Context.Company
                .Include(company => company.ActivitySector)
                .Include(company => company.Coordinates)
                .Include(company => company.Zoning);
        }

        public override int GetTotalCount (String companyName = null, int? zoningId = null)
        {
            return CompanyQueryBase()
                .Where(c =>
                    (zoningId == null || c.ZoningId == zoningId)
                    &&
                    (companyName == null || c.Name.ToLower().Contains(companyName.ToLower()))
                )
                .Count();
        }

        public override Task<IEnumerable<Company>> GetAllAsync(int? pageIndex = Constants.Page.Index, int? pageSize = Constants.Page.Size, String companyName = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Company>> GetAllInfoAsync(int? pageIndex = Constants.Page.Index, int? pageSize = Constants.Page.Size, int? zoningId = null, String companyName = null, String address = null, String activityName = null)
        {
            return Task.Run(() => CompanyQueryBase()
                .Where(company => 
                    (companyName == null || company.Name.ToLower().Contains(companyName.ToLower()))
                    &&
                    (zoningId == null || company.ZoningId == zoningId)
                    &&
                    (address == null || company.Address.ToLower().Contains(address.ToLower()))
                    &&
                    (activityName == null || company.ActivitySector.Name.ToLower().Contains(activityName.ToLower()))
                )
                .OrderBy(company => company.Id)
                .TakePage(pageIndex, pageSize)
                .AsEnumerable()
            );
        }

        public override Task<Company> FindByIdAsync(int id)
        {
            return CompanyQueryBase().FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<Company> AddAsync(Company data)
        {
            Context.Company.Add(data);
            await Context.SaveChangesAsync();
            return data;
        }

        public override async Task<Company> EditAsync(Company data)
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
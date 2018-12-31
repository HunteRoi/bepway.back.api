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

namespace DAL {
    public class CompanyDataAccess : DataAccess<Company> {
        public CompanyDataAccess (BepwayContext context) {
            Context = context;
        }

        private IIncludableQueryable<Company, ActivitySector> PrivateCompanyQueryBase () {
            return Context.Company.Include (company => company.ActivitySector);
        }

        private IIncludableQueryable<Company, Coordinates> CompanyQueryBase () {
            return PrivateCompanyQueryBase ().Include (company => company.Coordinates);
        }

        public override async Task<IEnumerable<Company>> GetAllAsync (int? pageIndex = 0, int? pageSize = 15, String companyName = null) {
            IEnumerable<Company> companies = await CompanyQueryBase ()
                .Where (company => companyName == null || company.Name.Contains (companyName))
                .OrderBy (company => company.Id)
                .TakePage (pageIndex.Value, pageSize.Value)
                .ToArrayAsync ();
            return companies;
        }

        public override async Task<Company> FindByIdAsync (int id) {
            Company entity = await CompanyQueryBase ().FirstOrDefaultAsync (c => c.Id == id);
            return entity;
        }
        private IEnumerable<Company> FindByName (string name) {
            IEnumerable<Company> companies = CompanyQueryBase ().Where (c => c.Name.Contains (name));
            return companies;
        }
        public async Task<Company> FindByAddressAsync (string address) {
            Company entity = await CompanyQueryBase ().FirstOrDefaultAsync (company => company.Address.Contains (address));
            return entity;
        }
        public override async Task<Company> AddAsync (Company data) {
            Context.Company.Add (data);
            await Context.SaveChangesAsync ();
            Company entity = FindByName (data.Name).FirstOrDefault ();
            return entity;
        }
        public async Task<Company> EditAsync (Company data) {
            if (Context.Entry (data).State == EntityState.Detached) {
                Context.Attach (data).State = EntityState.Modified;
            }
            Context.Entry (data).OriginalValues["RowVersion"] = data.RowVersion;
            await Context.SaveChangesAsync ();
            return data;
        }
        public override async Task DeleteAsync (Company data) {
            Context.Company.Remove (data);
            await Context.SaveChangesAsync ();
        }
    }
}
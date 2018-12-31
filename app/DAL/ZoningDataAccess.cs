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
    public class ZoningDataAccess : DataAccess<Zoning> {
        public ZoningDataAccess (BepwayContext context) {
            Context = context;
        }

        private IIncludableQueryable<Zoning, Coordinates> ZoningQueryBase () {
            return Context.Zoning.Include (zoning => zoning.Coordinates);
        }

        public override async Task<IEnumerable<Zoning>> GetAllAsync (int? pageIndex = 0, int? pageSize = 15, String zoningName = null) {
            IEnumerable<Zoning> companies = await ZoningQueryBase ()
                .Where (zoning => zoningName == null || zoning.Name.Contains (zoningName))
                .OrderBy (zoning => zoning.Id)
                .TakePage (pageIndex.Value, pageSize.Value)
                .ToArrayAsync ();
            return companies;
        }

        public override async Task<Zoning> FindByIdAsync (int id) {
            Zoning entity = await Context.Zoning.FindAsync (id);
            return entity;
        }
        private IEnumerable<Zoning> FindByName (string name) {
            IEnumerable<Zoning> companies = ZoningQueryBase ().Where (c => c.Name.Contains (name));
            return companies;
        }
        public override async Task<Zoning> AddAsync (Zoning data) {
            Context.Zoning.Add (data);
            await Context.SaveChangesAsync ();
            Zoning entity = FindByName (data.Name).FirstOrDefault ();
            return entity;
        }
        public async Task<Zoning> EditAsync (Zoning data) {
            if (Context.Entry (data).State == EntityState.Detached) {
                Context.Attach (data).State = EntityState.Modified;
            }
            await Context.SaveChangesAsync ();
            return data;
        }
        public override async Task DeleteAsync (Zoning data) {
            Context.Zoning.Remove (data);
            await Context.SaveChangesAsync ();
        }
    }
}
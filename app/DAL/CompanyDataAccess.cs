using Microsoft.EntityFrameworkCore;
using Model;

namespace DAL
{
    public class CompanyDataAccess : DataAccess<Company>
    {
        public CompanyDataAccess (DbContext context)
        {
            Context = context;
        }

        // public override async Task<Company> FindByIdAsync (int id) {}
        // public async Task<Company> FindByLogin (string login) {}
        // public override async Task<IEnumerable<Company>> GetAllAsync (int? pageIndex = 0, int? pageSize = 5, string companyName = null) {}
        // public override async Task<Company> AddAsync (Company data) {}
        // public override async Task<Company> DeleteAsync (int id) {}
        // public override async Task<Company> EditAsync (Company data) {}
    }
}
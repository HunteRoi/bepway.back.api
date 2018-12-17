using Microsoft.EntityFrameworkCore;
using Model;
using AutoMapper;

namespace DAL
{
    public class CompanyDataAccess : DataAccess<Company>
    {
        public CompanyDataAccess (BepwayContext context)
        {
            Context = context;
        }

        // public abstract Task<IEnumerable<Company>> GetAllAsync (int? pageIndex = 0, int? pageSize = 15, string companyName = null)
        // {}
        // public abstract Task<Company> FindByIdAsync (int id)
        // {}
        // public abstract Task AddAsync (Company data)
        // {}
        // protected abstract Task DeleteAsync (Company data)
        // {}
        // public abstract Task<Company> DeleteByIdAsync (int id)
        // {}
        // public abstract Task<Company> EditAsync (Company data, IMapper mapper);
        // {}
    }
}
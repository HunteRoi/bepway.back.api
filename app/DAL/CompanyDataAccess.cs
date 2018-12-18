// using Microsoft.EntityFrameworkCore;
// using Model;
// using AutoMapper;

// namespace DAL
// {
//     public class CompanyDataAccess : DataAccess<Company>
//     {
//         public CompanyDataAccess (BepwayContext context)
//         {
//             Context = context;
//         }
//         public abstract Task<IEnumerable<Company>> GetAllAsync (int? pageIndex = 0, int? pageSize = 15, string name = null);
//         public abstract Task<Company> FindByIdAsync (int id);
//         public abstract Task<Company> AddAsync (Company data);
//         public abstract Task DeleteAsync (Company data);
//     }
// }
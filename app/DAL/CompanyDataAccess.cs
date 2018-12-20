// using System;
// using Microsoft.EntityFrameworkCore;
// using Model;
// using System.Threading.Tasks;
// using System.Collections.Generic;

// namespace DAL
// {
//     public class CompanyDataAccess : DataAccess<Company>
//     {
//         public CompanyDataAccess (BepwayContext context)
//         {
//             Context = context;
//         }

//         public override async Task<IEnumerable<Company>> GetAllAsync (int? pageIndex = 0, int? pageSize = 15, String name = null)
//         {

//         }

//         public override async Task<Company> FindByIdAsync (int id)
//         {}

//         public override async Task<Company> AddAsync (Company data)
//         {}

//         public override async Task DeleteAsync (Company data)
//         {}
//     }
// }
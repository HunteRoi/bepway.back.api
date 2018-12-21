// using System;
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using Model;

// namespace DAL
// {
//     public class CompanyDataAccess : DataAccess<Company>
//     {
//         public CompanyDataAccess (BepwayContext context)
//         {
//             Context = context;
//             Client = new HttpClient();
//         }

//         public override async Task<IEnumerable<Company>> GetAllAsync (int? pageIndex = 0, int? pageSize = 15, String name = null)
//         {}

//         public override async Task<Company> FindByIdAsync (int id)
//         {}

//         public override async Task<Company> AddAsync (Company data)
//         {}

//         public override async Task DeleteAsync (Company data)
//         {}
//     }
// }
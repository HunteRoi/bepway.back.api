// using System;
// using System.Net;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Threading.Tasks;
// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using Newtonsoft.Json.Linq;
// using Model;

// namespace DAL
// {
//     public class CompanyDataAccess : DataAccess<Company>
//     {
//         private OpenDataAccess OpenDataAccess;    
//         private BepwayContext Context;
//         public CompanyDataAccess (BepwayContext context, OpenDataAccess ODA)
//         {
//             Context = context;
//             OpenDataAccess = ODA;
//         }

//         public override async Task<IEnumerable<Company>> GetAllAsync (int? pageIndex = 0, int? pageSize = 15, String companyName = null)
//         {}

//         public override async Task<Company> FindByIdAsync (int id)
//         {}

//         public override async Task<Company> AddAsync (Company data)
//         {}

//         public override async Task DeleteAsync (Company data)
//         {}
//     }
// }
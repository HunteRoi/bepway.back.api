using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DAL
{
    public abstract class DataAccess<T>
    {
        public DbContext Context { get; set; }
        //public ILogger Logger { get; set; }
        
        public abstract Task<IEnumerable<T>> GetAllAsync (int? pageIndex = 0, int? pageSize = 15, string name = null);
        public abstract Task<T> FindByIdAsync (int id);
        public abstract Task<T> AddAsync (T data);
        public abstract Task<T> DeleteAsync (int id);
        public abstract Task<T> EditAsync (T data); 
    }
}
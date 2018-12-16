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

        // public abstract Task<T> FindByIdAsync (int id);
        // public abstract Task<IEnumerable<T>> GetAllAsync (int pageSize = 10, int pageIndex = 0);
        // public abstract Task<T> AddAsync (T data);
        // public abstract Task<T> DeleteAsync (int id);
        // public abstract Task<T> EditAsync (T data); 
    }
}
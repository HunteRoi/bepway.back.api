using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;

namespace DAL
{
    public abstract class DataAccess<T>
    {
        public BepwayContext Context { get; set; }
        public ILogger Logger { get; set; }

        public abstract int GetTotalCount(String name = null);
        public abstract Task<IEnumerable<T>> GetAllAsync(int? pageIndex = Constants.Page.Index, int? pageSize = Constants.Page.Size, String name = null);
        public abstract Task<T> FindByIdAsync(int id);
        public abstract Task<T> AddAsync(T data);
        public abstract Task DeleteAsync(T data);
    }
}
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entities.Repo_interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetTableAsTracking();
        IQueryable<T> GetTableNoTracking();
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> SaveChangesAsync();

    }
}

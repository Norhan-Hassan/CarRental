using CarRental.DataAccess.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DataAccess.Repo_Implementations
{
    public class GenericRepo<T> where T : class
    {
        #region Properties

        protected readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;

        #endregion

        #region Constructors
        public GenericRepo(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }

        #endregion



        #region Functions
        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<T> GetTableNoTracking()
        {
            return _dbSet.AsNoTracking().AsQueryable();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #endregion
    }
}

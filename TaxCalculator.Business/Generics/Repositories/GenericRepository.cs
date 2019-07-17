using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Business.Generics.Interfaces;
using TaxCalculator.Data;

namespace TaxCalculator.Business.Generics.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        private readonly TaxCalculatorDbContext _dbContext;

        public GenericRepository(TaxCalculatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public Task<TEntity> FindById(int? id)
        {
            if (id == null)
                return null;

            return _dbContext.Set<TEntity>().FindAsync(id);
        }
        public Task<TEntity> FindById(string id)
        {
            if (id == null)
                return null;

            return _dbContext.Set<TEntity>().FindAsync(id);
        }
        public Task<TEntity> FindById(string id, DateTime beginDate)
        {
            if (id == null && beginDate == null)
                return null;

            return _dbContext.Set<TEntity>().FindAsync(id, beginDate);
        }
        public async Task Add(TEntity item)
        {
            await _dbContext.Set<TEntity>().AddAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddRange(List<TEntity> item)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(item);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(TEntity item)
        {
            _dbContext.Set<TEntity>().Update(item);
            await _dbContext.SaveChangesAsync();
        }
        public async Task Remove(TEntity item)
        {
            _dbContext.Set<TEntity>().Remove(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}

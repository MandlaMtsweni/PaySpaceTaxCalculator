using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxCalculator.Data;

namespace TaxCalculator.Business.Generics.Interfaces
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> FindById(int? id);
        Task<TEntity> FindById(string id);
        Task<TEntity> FindById(string id, DateTime beginDate);
        Task Add(TEntity item);
        Task Update(TEntity item);
        Task Remove(TEntity item);    

        Task AddRange(List<TEntity> item);
    }
}

using Market.Warehouse.Domain.Models;
using System.Security.AccessControl;

namespace Market.OrderProcessing.Application.Services.Base
{
    public interface IBaseEntityService<TEntity>
    where TEntity : Auditable
    {
        Task<int> CreateAsync(TEntity entity);
        Task DeleteAsync(int id);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetByIdAsync(int id);
        Task<int> UpdateAsync(TEntity entity);
    }
}

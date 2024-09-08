using Market.Warehouse.Domain.Models;

namespace E_CommerceProjectDemo.DataAccess.Repositories.Base;

public interface IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    Task<int> CreateAsync(TEntity entity);
    Task DeleteAsync(int id);
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(int id);
    Task<int> UpdateAsync(TEntity entity);
}

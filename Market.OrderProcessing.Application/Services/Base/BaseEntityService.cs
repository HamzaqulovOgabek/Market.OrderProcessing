using Market.OrderProcessing.Application.Context;
using Market.Warehouse.Domain.Enums;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Market.OrderProcessing.Application.Services.Base
{
    public abstract class BaseEntityService<TEntity> : IBaseEntityService<TEntity>
    where TEntity : Auditable, IHaveState
    {
        protected readonly OrderProcessingDbContext Context;

        public BaseEntityService(OrderProcessingDbContext dbContext)
        {
            Context = dbContext;
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            var entry = await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();

            return entity.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            entity.ModifiedAt = DateTime.UtcNow;
            entity.State = State.PASSIVE;
            await Context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            var entities = Context.Set<TEntity>().Where(e => e.State == State.ACTIVE);
            return entities;
        }

        public Task<TEntity> GetByIdAsync(int id)
        {
            var entity = Context.Set<TEntity>()
                .Where(e => e.State == State.ACTIVE)
                .FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                throw new Exception("Entity not found with this id");

            return entity;
        }

        public async Task<int> UpdateAsync(TEntity entity)
        {
            var entry = Context.Update(entity);
            await Context.SaveChangesAsync();

            return entity.Id;
        }

    }
}

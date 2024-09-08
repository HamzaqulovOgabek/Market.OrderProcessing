using Market.OrderProcessing.Application.Context;
using Market.Warehouse.DataAccess.Exceptions;
using Market.Warehouse.Domain.Enums;
using Market.Warehouse.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceProjectDemo.DataAccess.Repositories.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly OrderProcessingDbContext Context;

    public BaseRepository(OrderProcessingDbContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> GetAll()
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();
        if (typeof(IHaveState).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(x => ((IHaveState)x).State == State.ACTIVE);
        }
        return query;
    }
    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>().Where(x => x.Id.Equals(id));
        if (typeof(IHaveState).IsAssignableFrom(typeof(TEntity)))
        {
            query = query.Where(x => (x as IHaveState).State == State.ACTIVE);
            //query = query.Where(x => ((IHaveState)x).State == State.Active);
        }
        return await query.FirstOrDefaultAsync();
    }
    public async Task<int> CreateAsync(TEntity entity)
    {
        var entry = Context.Add(entity);
        entry.State = EntityState.Added;
        if (entity != null && entity is Auditable)
        {
            var auditableEntity = entity as Auditable;
            auditableEntity!.CreatedAt = DateTime.Now;
        }
        await Context.SaveChangesAsync();
        return entry.Entity.Id;
    }
    public async Task<int> UpdateAsync(TEntity entity)
    {
        Context.Update(entity);
        if (entity != null && entity is Auditable)
        {
            Auditable auditableEntity = entity as Auditable;
            auditableEntity.ModifiedAt = DateTime.Now;
        }
        await Context.SaveChangesAsync();
        return entity.Id;
    }
    public virtual async Task DeleteAsync(int id)
    {
        var entity = await Context.Set<TEntity>().FirstOrDefaultAsync(e => e.Id.Equals(id));
        if (entity == null)
        {
            throw new EntityNotFoundException($"Entity with id {id} not found.");
        }
        var haveState = entity as IHaveState;
        if (haveState != null)
        {
            //Soft delete
            haveState.State = State.PASSIVE;
            await Context.SaveChangesAsync();
            return;
        }
        //Hard delete
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }
}

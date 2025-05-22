using ItemService.Models;

namespace ItemService.Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseModel
    {
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(Guid id);
        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(Guid id);
    }
}

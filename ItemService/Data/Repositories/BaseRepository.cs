using ItemService.Data.Interfaces;
using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseModel
    {
        protected readonly DefaultDbContext _context;

        public BaseRepository(DefaultDbContext context)
        {
            _context = context;
        }

        public async Task Create(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entity = await GetById(id);
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().Where(e => !e.IsDeleted).AsNoTracking();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _context.Set<TEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}

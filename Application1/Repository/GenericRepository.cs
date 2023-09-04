using Application1.Data;
using Application1.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application1.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext db)
        {
            _db = db;
            this._dbSet = db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> villas = _dbSet;
            if (filter != null) villas = villas.Where(filter);
            return await villas.ToListAsync();
        }

        public async Task<T> GetTAsync(Expression<Func<T, bool>> filter = null, bool tracking = true)
        {
            IQueryable<T> entity = _dbSet;
            if (!tracking) entity = entity.AsNoTracking();
            if (filter != null) entity = entity.Where(filter);
            return await entity.FirstOrDefaultAsync();
        }



        public async Task RemoveAsync(int id)
        {
            var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            _dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }


    }
}

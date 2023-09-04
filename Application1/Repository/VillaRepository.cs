using Application1.Data;
using Application1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application1.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Villa villa)
        {
            await _db.Villas.AddAsync(villa);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateAsync(Villa villa)
        {
             _db.Villas.Update(villa);
            await _db.SaveChangesAsync();
        }


        public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa,bool>> filter = null)
        {
            IQueryable<Villa> villas = _db.Villas;
            if(filter != null) villas= villas.Where(filter);
            return await villas.ToListAsync();
        }

        public async Task<Villa> GetVillaAsync(Expression<Func<Villa, bool>> filter = null, bool tracking = true)
        {
            IQueryable<Villa> villas = _db.Villas;
            if(!tracking) villas = villas.AsNoTracking();
            if(filter != null) villas = villas.Where(filter);
            return await villas.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var villa = await _db.Villas.FirstOrDefaultAsync(x => x.Id == id);
           _db.Villas.Remove(villa);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
          await _db.SaveChangesAsync();
        }
    }
}

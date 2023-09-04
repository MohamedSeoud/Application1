using Application1.Data;
using Application1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application1.Repository
{
    public class VillaRepository : GenericRepository<Villa>, IVillaRepository 
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public async Task UpdateAsync(Villa villa)
        {
             _db.Villas.Update(villa);
            await _db.SaveChangesAsync();
        }



    }
}

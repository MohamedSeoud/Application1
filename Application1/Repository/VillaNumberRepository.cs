using Application1.Data;
using Application1.Models;
using Application1.Repository.IRepository;

namespace Application1.Repository
{
    public class VillaNumberRepository : GenericRepository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task Update(VillaNumber villaNumber)
        {
            _db.VillasNumber.Update(villaNumber);
            await _db.SaveChangesAsync();
        }
    }
}

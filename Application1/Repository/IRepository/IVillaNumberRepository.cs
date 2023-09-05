using Application1.Models;

namespace Application1.Repository.IRepository
{
    public interface IVillaNumberRepository:IGenericRepository<VillaNumber>
    {
         Task Update(VillaNumber villaNumber);
    }
}

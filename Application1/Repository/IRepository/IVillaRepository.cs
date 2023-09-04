using Application1.Models;
using System.Linq.Expressions;

namespace Application1.Repository
{
    public interface IVillaRepository
    {
        Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>>filter = null);
        Task<Villa> GetVillaAsync(Expression<Func<Villa, bool>> filter = null,bool tracking=true);
        Task CreateAsync(Villa villa);
        Task UpdateAsync(Villa villa);
        Task RemoveAsync(int id);
        Task SaveAsync();


    }
}

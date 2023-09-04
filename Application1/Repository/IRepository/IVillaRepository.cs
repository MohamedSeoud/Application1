using Application1.Models;
using Application1.Repository.IRepository;
using System.Linq.Expressions;

namespace Application1.Repository
{
    public interface IVillaRepository : IGenericRepository<Villa>
    {
        Task UpdateAsync(Villa villa);
    }
}

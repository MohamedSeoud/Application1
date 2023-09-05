using Application1.DTO_s;
using Application1.Models;

namespace Application1.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUser(string Name);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginData);
        Task<ApplicationUsers> Register (RegisterationModelDTO registerationModel); 

    }
}

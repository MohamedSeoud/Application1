using Application1.Data;
using Application1.DTO_s;
using Application1.Models;
using Application1.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUsers> _user;
        private string SecretKey;

        public UserRepository(ApplicationDbContext db,
            IMapper mapper,IConfiguration configuration,
            UserManager<ApplicationUsers> user)
        {
            _db = db;
            _mapper = mapper;
            SecretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
            _user = user;
        }
        public async Task<bool> IsUniqueUser(string Name)
        {
            var user = await _db.Users.FirstOrDefaultAsync(c=>c.UserName.ToLower() == Name.ToLower());
            if (user == null) return true;
            return false;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginData)
        {
            var user = await _db.Users.FirstOrDefaultAsync(c => c.UserName.ToLower() == loginData.UserName.ToLower());
            bool IsVaild = await _user.CheckPasswordAsync(user,loginData.Password);
            var roles = await _user.GetRolesAsync(user);
            if(user == null || IsVaild == false) 
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };

            };
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.Name,user.Id.ToString()),
                new Claim(ClaimTypes.Role,roles.FirstOrDefault()),

                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginRequestDTO = new LoginResponseDTO()
            {
                User = user,
                Token = tokenHandler.WriteToken(token)
            };
            return loginRequestDTO;
        }

        public async Task<ApplicationUsers> Register(RegisterationModelDTO registerationModel)
        {
            var user = _mapper.Map<ApplicationUsers>(registerationModel);
            await _db.AddAsync(user);
            await _db.SaveChangesAsync();
            return user;
        }
    }
}

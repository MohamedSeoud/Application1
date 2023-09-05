using Application1.DTO_s;
using Application1.Models;
using Application1.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Application1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController :ControllerBase
    {
        private readonly IUserRepository _user;
        public UserController(IUserRepository user)
        {
            _user = user;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginModel)
        {
            var response = new ApiResponseData<LoginResponseDTO>();
            try 
            { 
            var loginResponse = await _user.Login(loginModel);
            if(loginResponse == null)
            {
                response.ErrorMessage = new List<string>() { "UserName or Password isn't Correct" };
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                return Ok(response);
            }
                response.Data=loginResponse;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Found;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string>() { ex.Message };
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                return Ok(response);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterationModelDTO registerModel)
        {
            var response = new ApiResponseData<ApplicationUsers>();
            try
            {
                var user = await _user.Register(registerModel);
                if (user == null)
                {
                    response.ErrorMessage = new List<string>() { "You Should Put Invaild Data" };
                    response.IsSuccess = false;
                    response.StatusCode = HttpStatusCode.BadRequest;
                    return Ok(response);
                }
                response.Data = user;
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.Created;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = new List<string>() { ex.Message };
                response.IsSuccess = false;
                response.StatusCode = HttpStatusCode.NotFound;
                return Ok(response);
            }
        }
    }
}

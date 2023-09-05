using Application1.Models;

namespace Application1.DTO_s
{
    public class LoginResponseDTO
    {
        public ApplicationUsers User { get; set; }
        public string Token { get; set; }
    }
}

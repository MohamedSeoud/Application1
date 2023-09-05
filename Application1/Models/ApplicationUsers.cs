using Microsoft.AspNetCore.Identity;

namespace Application1.Models
{
    public class ApplicationUsers:IdentityUser
    {
        public string UserName { get; set;}
    }
}

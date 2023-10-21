using Microsoft.AspNetCore.Identity;

namespace UsersApi.Models
{
    public class User : IdentityUser
    {
        public DateTime DateofBirth { get; set; }

        public User() : base() { }
    }
}

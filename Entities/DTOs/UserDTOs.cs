
namespace Entities.DTOs
{
   
    
        public class RegisterUserDto
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;

        }
        public class LoginUserDto
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
  
        public class UserDto
        {
            public int Id { get; set; }
            public string Email { get; set; } = null!;
            public string? Password { get; set; } = null!;
        }
     
}

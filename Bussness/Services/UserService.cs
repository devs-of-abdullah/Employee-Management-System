using Data;
using Entities.DTOs;
using Business.Utils;
namespace Business
{
    public class UserService : IUserService
    {
        readonly IUserRepository _repo;
        readonly TokenService _tokenService;
        public UserService(IUserRepository repo, TokenService token)
        {
            _repo = repo;
            _tokenService = token;
        }

        public async Task<bool> Register(string email, string password)
        {
            bool existeduser = await _repo.IsExistsByEmail(email);

            if(existeduser == true)
                return false;


            var hashedPasssword = BCrypt.Net.BCrypt.HashPassword(password);
            var User = new RegisterUserDto()
            {
                
                Email = email,
                Password = hashedPasssword,
            };

             await _repo.Add(User);

             return true;
            
        }
        
        public async Task<string> Login(string email, string password) 
        { 
            var user = await _repo.GetByEmail(email);

            if(user == null) 
                return string.Empty;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            if(!isPasswordValid) 
                return string.Empty;

            var token = _tokenService.CreateToken(user);

            return token;
        }
       

    }
    
}

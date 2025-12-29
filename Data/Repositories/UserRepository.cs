
using Entities;
using Microsoft.EntityFrameworkCore;
using Entities.DTOs;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        readonly AppDbContext _context;
        public UserRepository(AppDbContext context) => _context = context;
        public async Task<UserDto?> GetByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email );
            
            if (user == null) return null;

            var foundUser = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,

            };
            return foundUser;
        }
        public async Task<UserDto?> GetById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return null;

            var foundUser = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,

            };
            return foundUser;
        } 

        public async Task<bool> IsExistsById(int id)
        {
             return await _context.Users.AnyAsync(u => u.Id == id);  

        }
        public async Task<bool> IsExistsByEmail(string email)
        {
            return await _context.Users.AnyAsync(u =>u.Email == email);
        }

        public async Task Add(RegisterUserDto user) 
        {
            var entity = new User
            {
                Email = user.Email,
                Password = user.Password,
            };

            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
        }


    }
}

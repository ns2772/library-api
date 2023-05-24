using LibraryApi.Infrastructure;
using LibraryApi.Interfaces;
using LibraryApi.Models;
using LibraryApi.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryApi.Services
{
    public class UserService : IUserService
    {
        private IRepository<Users> _userRepository;
        private SymmetricSecurityKey _secretKey;
        private readonly IConfiguration _configuration;

        public UserService(IRepository<Users> userRepository, IConfiguration configuration)
        {
            this._userRepository = userRepository;
            this._configuration = configuration;
        }
        public async Task<List<Users>> GetUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<Users> GetUserById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<Users> CreateUser(Users user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task<Users> UpdateUser(int id, Users updateUser)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.Name = updateUser.Name;
                user.Email = updateUser.Email;
                await _userRepository.UpdateAsync(user);
            }
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }
        }
        public async Task<Tokens> AuthenticateUser(LoginViewModel logins)
        {
            var foundUser = await _userRepository.GetAllAsync();
            if (!foundUser.Any(x => x.Email == logins.Email && x.Password == logins.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, logins.Email)
              }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };

        }

    }
}

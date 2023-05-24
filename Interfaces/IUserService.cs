using LibraryApi.Models;
using LibraryApi.ViewModels;

namespace LibraryApi.Interfaces
{
    public interface IUserService
    {
        Task<List<Users>> GetUsers();
        Task<Users> GetUserById(int id);
        Task<Users> CreateUser(Users user);
        Task<Users> UpdateUser(int id, Users updatedUser);
        Task DeleteUser(int id);
        Task<Tokens> AuthenticateUser(LoginViewModel logins);
    }
}

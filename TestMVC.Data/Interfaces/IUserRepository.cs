using System.Collections.Generic;
using System.Threading.Tasks;
using TestMVC.Model.DTO.User;

namespace TestMVC.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<UserDTO>> GetAllAsync();

        Task<UserDTO> GetAsync(int id);

        Task<bool> UsernameExistsAsync(int? id, string username);

        Task SaveAsync(string email, string username, string hashPassword, char genre);

        Task UpdatePasswordAsync(int id, string hashPassword);

        Task UpdateUserAsync(int id, string username, string email);

        Task ChangeStatusAsync(int id, bool status);

        Task DeleteAsync(int id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using TestMVC.Model.Common;
using TestMVC.Model.User;

namespace TestMVC.Business.Interfaces
{
    public interface IUserBusiness
    {
        Task<ICollection<UserViewModel>> GetAllAsync();

        Task<UserViewModel> GetAsync(int id);

        Task<OperationResult> SaveAsync(UserViewModel request);

        Task<OperationResult> ChangeStatusAsync(int id, bool status);

        Task<OperationResult> DeleteAsync(int id);
    }
}

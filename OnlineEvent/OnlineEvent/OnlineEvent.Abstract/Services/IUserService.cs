using OnlineEvent.Core.Entities;
using OnlineEvent.Core.EntityConst;
using OnlineEvent.Model;
using OnlineEvent.Model.AppUserModels;
using OnlineEvent.Model.EventModels;
using System.Linq.Expressions;

namespace OnlineEvent.Abstract.Services
{
    public interface IUserService
    {
        // Sonradan eklenen düzeltmeler
        Task<CustomResponseModel<AppUserModel>> CreateUserAsync(UserCreateModel userCreateModel);
        Task<CustomResponseModel<AppUserModel>> GetUserByNameAsync(string userName);


        Task<CustomResponseModel<AppUserModel>> GetByIdAsync(int id);

        Task<CustomResponseModel<IEnumerable<AppUserModel>>> GetAllAsync();
        Task<CustomResponseModel<IEnumerable<AppUserModel>>> GetAllAsync(string types);
        Task<bool> AnyAsync(Expression<Func<AppUser, bool>> expression);
        Task<CustomResponseModel<NoContentModel>> ChangeEmailAsync(string userName, string newEmail);
        Task<CustomResponseModel<NoContentModel>> ChangePasswordAsync(string userName, string currentPassword,string newPassword);

    }
}

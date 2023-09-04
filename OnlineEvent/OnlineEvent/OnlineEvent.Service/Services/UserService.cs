using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Abstract.UnitOfWorks;
using OnlineEvent.Core.Entities;
using OnlineEvent.Core.EntityConst;
using OnlineEvent.Model;
using OnlineEvent.Model.AppUserModels;
using System.Linq.Expressions;

namespace OnlineEvent.Service.Services
{
    public class UserService :IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleManager = roleManager;
        }

        public async Task<bool> AnyAsync(Expression<Func<AppUser, bool>> expression)
        {
           bool user = await _userManager.Users.AnyAsync(expression);
           return user;
        }

        public async Task<CustomResponseModel<NoContentModel>> ChangeEmailAsync(string userName, string newEmail)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return CustomResponseModel<NoContentModel>.Failure(404, "UserName bulunamadı");
            }
            string token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            var result = await _userManager.ChangeEmailAsync(user, newEmail, token);
            if(!result.Succeeded)
            {
                return CustomResponseModel<NoContentModel>.Failure(500, "Bir hata meydana geldi işlem başarısız");
            }
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(204);
        }

        public async Task<CustomResponseModel<NoContentModel>> ChangePasswordAsync(string userName, string currentPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return CustomResponseModel<NoContentModel>.Failure(404, "UserName bulunamadı");
            }
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                return CustomResponseModel<NoContentModel>.Failure(400, "Hatalı şifre");
            }
            await _unitOfWork.CommitAsync();

            return CustomResponseModel<NoContentModel>.Success(204);


        }

        public async Task<CustomResponseModel<IEnumerable<AppUserModel>>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var dtoUsers = new List<AppUserModel>();
            users.ForEach(entity => { dtoUsers.Add(MapToDto(entity)); });
            return CustomResponseModel<IEnumerable<AppUserModel>>.Success(200, dtoUsers);
        }

        public async Task<CustomResponseModel<AppUserModel>> GetByIdAsync(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var dtoEntity = MapToDto(user);
            return CustomResponseModel<AppUserModel>.Success(200, dtoEntity);
        }
        public async Task<CustomResponseModel<AppUserModel>> CreateUserAsync(UserCreateModel userCreateModel)
        {
            var user = new AppUser
            {
                Name = userCreateModel.Name,
                Surname = userCreateModel.Surname,
                UserName = userCreateModel.UserName,
                Email = userCreateModel.Email,
                PhoneNumber = userCreateModel.PhoneNumber,
                UserType = userCreateModel.UserType,
                InstitutionName = userCreateModel.InstitutionName,
                InstitutionWebSite = userCreateModel.InstitutionWebSite,

            };
            var result = await _userManager.CreateAsync(user,userCreateModel.Password.Trim());
            if(!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();
                return CustomResponseModel<AppUserModel>.Failure(400, errors);
            }
            var type = userCreateModel.UserType;
            if (!_roleManager.RoleExistsAsync($"{type}").Result)
            {
                AppRole role = new AppRole()
                {
                    Name = $"{type}"
                };
                IdentityResult roleResult = await _roleManager.CreateAsync(role);
                if (roleResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, $"{type}");
                }
            }
            var mapping = _mapper.Map<AppUserModel>(user);
            return CustomResponseModel<AppUserModel>.Success(201, mapping);
        }
        public async Task<CustomResponseModel<AppUserModel>> GetUserByNameAsync(string userName)
        {
           var user = await _userManager.FindByNameAsync(userName);
            if(user == null)
            {
                return CustomResponseModel<AppUserModel>.Failure(404,"UserName bulunamadı");
            }
            var mapping = _mapper.Map<AppUserModel>(user);

            return CustomResponseModel<AppUserModel>.Success(200, mapping);
        }

        private AppUserModel MapToDto(AppUser user)
        {
            return _mapper.Map<AppUserModel>(user);
        }

        public async Task<CustomResponseModel<IEnumerable<AppUserModel>>> GetAllAsync(string types)
        {
            var users = await _userManager.Users.Where(x=>x.UserType == types).ToListAsync();
            var dtoUsers = new List<AppUserModel>();
            users.ForEach(entity => { dtoUsers.Add(MapToDto(entity)); });
            return CustomResponseModel<IEnumerable<AppUserModel>>.Success(200, dtoUsers);
        }
    }
}

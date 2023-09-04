using Microsoft.AspNetCore.Identity;
using OnlineEvent.Model.AppUserModels;
using OnlineEvent.Model;
using OnlineEvent.Model.AppRoleModels;
using OnlineEvent.Core.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace OnlineEvent.Service.Services
{
    public class UserRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mapper;

        public UserRoleService(RoleManager<AppRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<CustomResponseModel<IEnumerable<AppRoleModel>>> GetRoleListAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var dtoRoles = new List<AppRoleModel>();
            roles.ForEach(r => { dtoRoles.Add(MapToDto(r)); });
            return CustomResponseModel<IEnumerable<AppRoleModel>>.Success(200, dtoRoles);

        }
        //public async Task<CustomResponseModel<IEnumerable<AppUserModel>>> GetInstitutionListAsync()
        //{
        //    var users = await _userManager.Users.ToListAsync();
        //    var dtoUsers = new List<AppUserModel>();
        //    users.ForEach(entity => { dtoUsers.Add(MapToDto(entity)); });
        //    return CustomResponseModel<IEnumerable<AppUserModel>>.Success(200, dtoUsers);
        //}
        //public async Task<CustomResponseModel<IEnumerable<AppUserModel>>> GetInstitutionListAsync()
        //{
        //    var users = await _userManager.Users.ToListAsync();
        //    var dtoUsers = new List<AppUserModel>();
        //    users.ForEach(entity => { dtoUsers.Add(MapToDto(entity)); });
        //    return CustomResponseModel<IEnumerable<AppUserModel>>.Success(200, dtoUsers);
        //}
        private AppRoleModel MapToDto(AppRole role)
        {
            return _mapper.Map<AppRoleModel>(role);
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Core.EntityConst;
using OnlineEvent.Model.AppUserModels;

namespace OnlineEvent.API.Controllers
{

    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser(UserCreateModel model)
        {
            var user = await _userService.CreateUserAsync(model);
            return CreateActionResult(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var users = await _userService.GetAllAsync();
            return CreateActionResult(users);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return CreateActionResult(user);
        }
        [Authorize]
        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangeEmail(ChangeEmailModel model)
        {    var userName = HttpContext.User.Identity.Name;

            var change = await _userService.ChangeEmailAsync(userName, model.Email);
             return CreateActionResult(change);
        }
        [Authorize]
        [HttpPatch("[action]")]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            var userName = HttpContext.User.Identity.Name;
            var change = await _userService.ChangePasswordAsync(userName,model.OldPassword,model.NewPassword);
            return CreateActionResult(change);
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUser()
        {
            // Token içerisinden name 'i bulmak için
            var user = await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            return CreateActionResult(user);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("type/{types}")]
        public async Task<IActionResult> GetUsersWithType(string types)
        {
            var users = await _userService.GetAllAsync(types);
            return CreateActionResult(users);
        }
    }
}

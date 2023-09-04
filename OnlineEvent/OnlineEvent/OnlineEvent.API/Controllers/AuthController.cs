using Microsoft.AspNetCore.Mvc;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Model.AppUserModels;
using OnlineEvent.Model.TokenModels;

namespace OnlineEvent.API.Controllers
{
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateToken(LoginModel loginModel)
            {
            var result = await _authenticationService.CreateTokenAsync(loginModel);
            return CreateActionResult(result);
        }
        //[HttpPost("[action]")]
        //public IActionResult CreateTokenByClient(ClientLoginModel clientLoginModel)
        //{
        //    var result = _authenticationService.CreateTokenByClient(clientLoginModel);
        //    return CreateActionResult(result);
        //}
        [HttpPost("[action]")]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenModel token)
        {
            var result = await _authenticationService.RevokeRefreshTokenAsync(token.Token);
            return CreateActionResult(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenModel token)
        {
            var result = await _authenticationService.CreateTokenByRefreshTokenAsync(token.Token);
            return CreateActionResult(result);

        }
    }
}

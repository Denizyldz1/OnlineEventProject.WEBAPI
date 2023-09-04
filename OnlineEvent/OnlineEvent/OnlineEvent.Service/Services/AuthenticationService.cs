using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Abstract.Services;
using OnlineEvent.Abstract.UnitOfWorks;
using OnlineEvent.Core.Configurations;
using OnlineEvent.Core.Entities;
using OnlineEvent.Model;
using OnlineEvent.Model.AppUserModels;
using OnlineEvent.Model.TokenModels;

namespace OnlineEvent.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationRepository _userRepository;

        public AuthenticationService(IOptions<List<Client>> optionsClient,
                                     ITokenService tokenService,
                                     UserManager<AppUser> userManager,
                                     IUnitOfWork unitOfWork,
                                     IAuthenticationRepository userRepository)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<CustomResponseModel<TokenModel>> CreateTokenAsync(LoginModel loginModel)
        {
            if(loginModel == null) throw new ArgumentNullException(nameof(loginModel));
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if(user == null) if (user == null) return CustomResponseModel<TokenModel>.Failure(400, "Email or Password is wrong");

            if (!await _userManager.CheckPasswordAsync(user, loginModel.Password)) // False ise
            {
                return CustomResponseModel<TokenModel>.Failure(400, "Email or Password is wrong");
            }
            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRepository.Where(x=>x.UserId==user.Id).SingleOrDefaultAsync();

            if(userRefreshToken == null)
            {
                await _userRepository.AddAsync(new UserRefreshToken { 
                    UserId = user.Id,
                    RefreshToken =token.RefreshToken,
                    Expiration = token.RefreshTokenExpiration
                });
            }
            else
            {
                userRefreshToken.RefreshToken = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<TokenModel>.Success(200, token);
        }

        public CustomResponseModel<ClientTokenModel> CreateTokenByClient(ClientLoginModel clientLoginModel)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginModel.ClientId && x.Secret == clientLoginModel.ClientSecret);
            if (client == null)
            {
                return CustomResponseModel<ClientTokenModel>.Failure(404,"ClientId or ClientSecret not found");
            }
            var token = _tokenService.CreateClientToken(client);

            return CustomResponseModel<ClientTokenModel>.Success(200,token);
        }

        public async Task<CustomResponseModel<TokenModel>> CreateTokenByRefreshTokenAsync(string refreshToken)
        {

            var existRefreshToken = await _userRepository.Where(x => x.RefreshToken == refreshToken).SingleOrDefaultAsync();

            if(existRefreshToken == null)
            {
                return CustomResponseModel<TokenModel>.Failure(404, "Refresh token not found");
            }
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId.ToString());
            if (user == null)
            {
                return CustomResponseModel<TokenModel>.Failure(404, "UserId not found");

            }

            var tokenDto = _tokenService.CreateToken(user);
            existRefreshToken.RefreshToken = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();
            return CustomResponseModel<TokenModel>.Success(200,tokenDto);
        }

        public async Task<CustomResponseModel<NoContentModel>> RevokeRefreshTokenAsync(string refreshToken)
        {
            var existRefreshToken = await _userRepository.Where(x => x.RefreshToken == refreshToken).SingleOrDefaultAsync();
            if(existRefreshToken == null)
            {
                return CustomResponseModel<NoContentModel>.Failure(404, "Refresh token not found");

            }
            _userRepository.Remove(existRefreshToken);
            await _unitOfWork.CommitAsync();
            return CustomResponseModel<NoContentModel>.Success(204);

        }
    }
}

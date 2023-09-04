using OnlineEvent.Model;
using OnlineEvent.Model.AppUserModels;
using OnlineEvent.Model.TokenModels;

namespace OnlineEvent.Abstract.Services
{

    public interface IAuthenticationService
    {
        // Kullanıcı bilgilerini alıp geriye token döneceğiz
        Task<CustomResponseModel<TokenModel>> CreateTokenAsync(LoginModel loginModel);
        Task<CustomResponseModel<TokenModel>> CreateTokenByRefreshTokenAsync(string refreshToken);
        // Refresh token sonlandırma veritabanında null çekme
        Task<CustomResponseModel<NoContentModel>> RevokeRefreshTokenAsync(string refreshToken);
        // Client ile birlikte token alma 
        CustomResponseModel<ClientTokenModel> CreateTokenByClient(ClientLoginModel clientLoginModel);
    }
}

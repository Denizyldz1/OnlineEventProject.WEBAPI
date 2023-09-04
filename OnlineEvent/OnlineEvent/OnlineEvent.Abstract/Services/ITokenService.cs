using OnlineEvent.Core.Configurations;
using OnlineEvent.Core.Entities;
using OnlineEvent.Model.TokenModels;

namespace OnlineEvent.Abstract.Services
{
    public interface ITokenService
    {
        TokenModel CreateToken(AppUser user);
        ClientTokenModel CreateClientToken(Client client);
    }
}

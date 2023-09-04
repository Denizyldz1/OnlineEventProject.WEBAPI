using OnlineEvent.Abstract.Repositories;
using OnlineEvent.Core.Entities;

namespace OnlineEvent.Data.Repositories
{
    public class AuthenticationRepository : GenericRepository<UserRefreshToken>, IAuthenticationRepository
    {
        public AuthenticationRepository(AppDbContext context) : base(context)
        {
        }
    }
}

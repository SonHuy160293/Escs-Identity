using Core.Infrastructure.Persistence;
using Identity.Domain.Interfaces;
using Identity.Domain.Model;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence
{
    public class UserTokenRepository : GenericRepository<UserToken>, IUserTokenRepository
    {
        public UserTokenRepository(IdentityDbContext context, ILogger<UserTokenRepository> logger) : base(context, logger)
        {
        }
    }
}

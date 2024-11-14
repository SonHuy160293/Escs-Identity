using Core.Infrastructure.Persistence;
using Identity.Domain.Interfaces;
using Identity.Domain.Model;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IdentityDbContext context, ILogger<UserRepository> logger) : base(context, logger)
        {
        }
    }
}

using Core.Infrastructure.Persistence;
using Identity.Domain.Interfaces;
using Identity.Domain.Model;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IdentityDbContext context, ILogger<RoleRepository> logger) : base(context, logger)
        {
        }
    }
}

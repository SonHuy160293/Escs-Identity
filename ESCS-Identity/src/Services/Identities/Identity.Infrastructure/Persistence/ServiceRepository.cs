using Core.Infrastructure.Persistence;
using Identity.Domain.Interfaces;
using Identity.Domain.Model;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence
{
    public class ServiceRepository : GenericRepository<Service>, IServiceRepository
    {
        public ServiceRepository(IdentityDbContext context, ILogger<ServiceRepository> logger) : base(context, logger)
        {
        }
    }
}

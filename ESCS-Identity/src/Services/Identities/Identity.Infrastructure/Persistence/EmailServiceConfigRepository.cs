using Core.Infrastructure.Persistence;
using Identity.Domain.Interfaces;
using Identity.Domain.Model;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Persistence
{
    public class EmailServiceConfigRepository : GenericRepository<EmailServiceConfig>, IEmailServiceConfigRepository
    {
        public EmailServiceConfigRepository(IdentityDbContext context, ILogger<EmailServiceConfigRepository> logger) : base(context, logger)
        {
        }
    }
}

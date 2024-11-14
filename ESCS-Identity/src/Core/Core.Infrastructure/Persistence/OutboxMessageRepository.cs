using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Persistence
{
    public class OutboxMessageRepository : GenericRepository<OutboxMessage>, IGenericRepository<OutboxMessage>
    {


        public OutboxMessageRepository(DbContext context, ILogger<OutboxMessageRepository> logger) : base(context, logger)
        {

        }
    }
}

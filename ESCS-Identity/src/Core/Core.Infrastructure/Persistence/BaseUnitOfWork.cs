using Core.Domain.Interfaces;
using Core.MessageBroker.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Core.Infrastructure.Persistence
{
    public class BaseUnitOfWork : IBaseUnitOfWork
    {
        private readonly BaseDbContext _context;
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IIntegrationEventBuilder _integrationEventBuilder;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public BaseUnitOfWork(BaseDbContext context, IServiceProvider provider, IIntegrationEventBuilder integrationEventBuilder,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _outboxMessageRepository = (IOutboxMessageRepository)provider.GetService(typeof(IOutboxMessageRepository));
            _integrationEventBuilder = integrationEventBuilder;
            _httpContextAccessor = httpContextAccessor;
        }

        public IOutboxMessageRepository OutboxMessage => _outboxMessageRepository;

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangesAsync()
        {
            try
            {

                _context.CheckDomainEvents(_integrationEventBuilder);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}

using Core.MessageBroker.Entities;

namespace Core.MessageBroker.Interfaces
{
    public interface IIntegrationEventBuilder
    {
        public BaseIntegrationEvent GetIntegrationEvent(Core.Domain.Base.DomainEvent domainEvent);
        public string GetQueueName(BaseIntegrationEvent integrationEvent);
    }
}

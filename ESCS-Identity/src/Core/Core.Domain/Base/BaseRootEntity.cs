using Core.Domain.Helpers;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Core.Domain.Base
{
    public abstract class BaseRootEntity : BaseEntity
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseRootEntity(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [JsonIgnore]
        private List<DomainEvent> _events;

        public IReadOnlyCollection<DomainEvent> DomainEvents => _events?.AsReadOnly();
        protected BaseRootEntity()
        {

        }

        public void AddDomainEvent(DomainEvent @event)
        {
            if (_events is null)
                _events = new List<DomainEvent>();

            @event.CorrelationId = CorrelationIdProvider.GetCorrelationId();
            _events.Add(@event);
        }

        public void RemoveDomainEvent(DomainEvent @event)
        {
            _events?.Remove(@event);
        }

        public void ClearDomainEvents()
        {
            _events?.Clear();
        }

    }
}

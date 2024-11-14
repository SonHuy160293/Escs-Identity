namespace Core.Domain.Base
{
    public interface DomainEvent
    {
        public string CorrelationId { get; set; }
    }
}

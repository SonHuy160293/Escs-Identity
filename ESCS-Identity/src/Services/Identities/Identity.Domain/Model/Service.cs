using Core.Domain.Base;

namespace Identity.Domain.Model
{
    public class Service : BaseEntity
    {

        public string Name { get; set; } = default!;
        public string? BaseUrl = default!;
    }
}

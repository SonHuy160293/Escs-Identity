using Core.Domain.Base;

namespace Identity.Domain.Model
{
    public class Role : BaseEntity
    {

        public string Name { get; set; } = default!;
    }
}

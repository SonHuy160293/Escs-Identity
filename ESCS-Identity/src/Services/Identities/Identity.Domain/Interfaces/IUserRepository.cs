using Core.Domain.Interfaces;
using Identity.Domain.Model;

namespace Identity.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}

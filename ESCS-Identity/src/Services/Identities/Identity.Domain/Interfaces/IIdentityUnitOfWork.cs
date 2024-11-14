using Core.Domain.Interfaces;

namespace Identity.Domain.Interfaces
{
    public interface IIdentityUnitOfWork : IBaseUnitOfWorks
    {
        IUserRepository UserRepository { get; }
        IServiceRepository ServiceRepository { get; }
        IUserTokenRepository UserTokenRepository { get; }
        IRoleRepository RoleRepository { get; }
        IEmailServiceConfigRepository EmailServiceConfigRepository { get; }
    }
}

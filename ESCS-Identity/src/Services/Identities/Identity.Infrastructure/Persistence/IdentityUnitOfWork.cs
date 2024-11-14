using Core.Infrastructure.Persistence;
using Identity.Domain.Interfaces;

namespace Identity.Infrastructure.Persistence
{
    public class IdentityUnitOfWork : UnitOfWorks, IIdentityUnitOfWork
    {

        private readonly IUserRepository _userRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IEmailServiceConfigRepository _emailServiceConfigRepository;
        private readonly IServiceRepository _serviceRepository;

        public IdentityUnitOfWork(IdentityDbContext context, IServiceProvider provider) : base(context, provider)
        {
            _userRepository = (IUserRepository)provider.GetService(typeof(IUserRepository));
            _emailServiceConfigRepository = (IEmailServiceConfigRepository)provider.GetService(typeof(IEmailServiceConfigRepository));
            _roleRepository = (IRoleRepository)provider.GetService(typeof(IRoleRepository));
            _serviceRepository = (IServiceRepository)provider.GetService(typeof(IServiceRepository));
            _userTokenRepository = (IUserTokenRepository)provider.GetService(typeof(IUserTokenRepository));
        }

        public IUserRepository UserRepository => _userRepository;

        public IServiceRepository ServiceRepository => _serviceRepository;

        public IUserTokenRepository UserTokenRepository => _userTokenRepository;

        public IRoleRepository RoleRepository => _roleRepository;

        public IEmailServiceConfigRepository EmailServiceConfigRepository => _emailServiceConfigRepository;
    }
}

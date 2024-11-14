using AutoMapper;
using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Domain.Interfaces;
using Identity.Domain.Model;

namespace Identity.Application.Features.Commands.Roles
{
    internal class CreateRole
    {
    }
    public class CreateRoleCommand : ICommand<BaseResult>
    {
        public string Name { get; set; } = default!;

    }

    public class CreateRoleHandler : ICommandHandler<CreateRoleCommand, BaseResult>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;

        public CreateRoleHandler(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {

            var role = _mapper.Map<Role>(request);

            await _identityUnitOfWork.RoleRepository.Add(role);

            await _identityUnitOfWork.SaveChangesAsync();

            return BaseResult.Success();
        }
    }
}
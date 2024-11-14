using AutoMapper;
using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Domain.Dtos;
using Identity.Domain.Interfaces;

namespace Identity.Application.Features.Queries.Roles
{
    internal class GetAllRoles
    {
    }

    public class GetAllRolesQuery : IQuery<BaseResult<IEnumerable<RoleGetDto>>>
    {
    }

    // Implement the handler using the correct response type
    public class GetAllRolesHandler : IQueryHandler<GetAllRolesQuery, BaseResult<IEnumerable<RoleGetDto>>>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllRolesHandler(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<IEnumerable<RoleGetDto>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {

            var roles = _mapper.Map<IEnumerable<RoleGetDto>>(await _identityUnitOfWork.RoleRepository.FindByQuery(null, false));

            return BaseResult<IEnumerable<RoleGetDto>>.Success(roles);
        }
    }

}

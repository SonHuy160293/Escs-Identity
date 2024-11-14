using AutoMapper;
using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Domain.Dtos;
using Identity.Domain.Interfaces;

namespace Identity.Application.Features.Queries.Users
{
    internal class GetAllUser
    {
    }

    public class GetAllUsersQuery : IQuery<BaseResult<IEnumerable<UserGetDto>>>
    {
    }

    // Implement the handler using the correct response type
    public class GetAllUsersHandler : IQueryHandler<GetAllUsersQuery, BaseResult<IEnumerable<UserGetDto>>>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllUsersHandler(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<IEnumerable<UserGetDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {

            var users = _mapper.Map<IEnumerable<UserGetDto>>(await _identityUnitOfWork.UserRepository.FindByQuery(null, false, u => u.Role));

            return BaseResult<IEnumerable<UserGetDto>>.Success(users);
        }
    }
}

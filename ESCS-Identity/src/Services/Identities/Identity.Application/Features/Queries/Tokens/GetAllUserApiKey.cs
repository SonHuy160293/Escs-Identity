using AutoMapper;
using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Domain.Dtos;
using Identity.Domain.Interfaces;

namespace Identity.Application.Features.Queries.Tokens
{
    internal class GetAllUserApiKey
    {
    }

    public class GetAllUserApiKeyQuery : IQuery<BaseResult<IEnumerable<UserTokenGetDto>>>
    {
    }

    // Implement the handler using the correct response type
    public class GetAllUserTokensHandler : IQueryHandler<GetAllUserApiKeyQuery, BaseResult<IEnumerable<UserTokenGetDto>>>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllUserTokensHandler(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<IEnumerable<UserTokenGetDto>>> Handle(GetAllUserApiKeyQuery request, CancellationToken cancellationToken)
        {

            var userTokens = _mapper.Map<IEnumerable<UserTokenGetDto>>(await _identityUnitOfWork.UserTokenRepository.FindByQuery(null, false, ut => ut.User, ut => ut.Service));

            return BaseResult<IEnumerable<UserTokenGetDto>>.Success(userTokens);
        }
    }
}

using AutoMapper;
using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Domain.Dtos;
using Identity.Domain.Interfaces;

namespace Identity.Application.Features.Queries.EmailServiceConfigs
{
    internal class GetAllEmailServiceConfig
    {
    }

    public class GetAllEmailServiceConfigsQuery : IQuery<BaseResult<IEnumerable<EmailServiceConfigGetDto>>>
    {
    }

    // Implement the handler using the correct response type
    public class GetAllEmailServiceConfigsHandler : IQueryHandler<GetAllEmailServiceConfigsQuery, BaseResult<IEnumerable<EmailServiceConfigGetDto>>>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllEmailServiceConfigsHandler(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<IEnumerable<EmailServiceConfigGetDto>>> Handle(GetAllEmailServiceConfigsQuery request, CancellationToken cancellationToken)
        {

            var emailServiceConfigs = _mapper.Map<IEnumerable<EmailServiceConfigGetDto>>(await _identityUnitOfWork.EmailServiceConfigRepository.FindByQuery(null, false, es => es.User, es => es.Service));

            return BaseResult<IEnumerable<EmailServiceConfigGetDto>>.Success(emailServiceConfigs);
        }
    }
}

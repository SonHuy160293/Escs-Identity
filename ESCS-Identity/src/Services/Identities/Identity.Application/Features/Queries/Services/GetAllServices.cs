using AutoMapper;
using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Domain.Dtos;
using Identity.Domain.Interfaces;

namespace Identity.Application.Features.Queries.Services
{
    internal class GetAllServices
    {
    }

    public class GetAllServicesQuery : IQuery<BaseResult<IEnumerable<ServiceGetDto>>>
    {
    }

    // Implement the handler using the correct response type
    public class GetAllServicesHandler : IQueryHandler<GetAllServicesQuery, BaseResult<IEnumerable<ServiceGetDto>>>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;

        public GetAllServicesHandler(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult<IEnumerable<ServiceGetDto>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {

            var services = _mapper.Map<IEnumerable<ServiceGetDto>>(await _identityUnitOfWork.ServiceRepository.FindByQuery(null, false));

            return BaseResult<IEnumerable<ServiceGetDto>>.Success(services);
        }
    }
}

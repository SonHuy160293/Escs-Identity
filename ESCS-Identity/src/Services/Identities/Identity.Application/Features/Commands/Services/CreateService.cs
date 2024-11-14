using AutoMapper;
using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Domain.Interfaces;
using Identity.Domain.Model;

namespace Identity.Application.Features.Commands.Services
{
    internal class CreateService
    {
    }

    public class CreateServiceCommand : ICommand<BaseResult>
    {
        public string Name { get; set; } = default!;
        public string? BaseUrl = default!;
    }

    public class CreateServiceHandler : ICommandHandler<CreateServiceCommand, BaseResult>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;

        public CreateServiceHandler(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
        {

            var service = _mapper.Map<Service>(request);

            await _identityUnitOfWork.ServiceRepository.Add(service);

            await _identityUnitOfWork.SaveChangesAsync();

            return BaseResult.Success();
        }
    }
}

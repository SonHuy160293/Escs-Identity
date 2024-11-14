using AutoMapper;
using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Domain.Interfaces;

namespace Identity.Application.Features.Commands.EmailServiceConfig
{
    internal class CreateEmailServiceConfig
    {
    }
    public class CreateEmailServiceConfigCommand : ICommand<BaseResult>
    {
        public string SmtpEmail { get; set; } = default!;
        public string SmtpPassword { get; set; } = default!;
        public int SmtpPort { get; set; }

        public long UserId { get; set; }

        public long ServiceId { get; set; }


    }

    public class CreateEmailServiceConfigHandler : ICommandHandler<CreateEmailServiceConfigCommand, BaseResult>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;

        public CreateEmailServiceConfigHandler(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult> Handle(CreateEmailServiceConfigCommand request, CancellationToken cancellationToken)
        {

            var emailServiceConfig = _mapper.Map<Identity.Domain.Model.EmailServiceConfig>(request);

            await _identityUnitOfWork.EmailServiceConfigRepository.Add(emailServiceConfig);

            await _identityUnitOfWork.SaveChangesAsync();

            return BaseResult.Success();
        }
    }
}

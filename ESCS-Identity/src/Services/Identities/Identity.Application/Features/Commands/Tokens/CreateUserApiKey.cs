using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Application.Exceptions;
using Identity.Application.Extensions;
using Identity.Domain.Interfaces;
using Identity.Domain.Model;

namespace Identity.Application.Features.Commands.Tokens
{
    internal class CreateUserApiKey
    {
    }

    public class CreateUserApiKeyCommand : ICommand<BaseResult>
    {
        public long UserId { get; set; }
        public long ServiceId { get; set; }
    }

    public class CreateUserApiKeyHandler : ICommandHandler<CreateUserApiKeyCommand, BaseResult>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;

        public CreateUserApiKeyHandler(IIdentityUnitOfWork identityUnitOfWork)
        {
            _identityUnitOfWork = identityUnitOfWork;
        }

        public async Task<BaseResult> Handle(CreateUserApiKeyCommand request, CancellationToken cancellationToken)
        {

            List<string> roles = new List<string>()
            {
                "User"
            };

            var user = await _identityUnitOfWork.UserRepository.GetById(request.UserId);

            if (user is null)
            {
                throw new NotFoundException("User not found");
            }

            var token = TokenExtension.CreateAccessToken(user, roles, request.ServiceId, false);

            var userToken = new UserToken()
            {
                ServiceId = request.ServiceId,
                IsActive = true,
                Key = token,
                UserId = request.UserId,

            };

            await _identityUnitOfWork.UserTokenRepository.Add(userToken);
            await _identityUnitOfWork.SaveChangesAsync();

            return BaseResult.Success();
        }
    }
}

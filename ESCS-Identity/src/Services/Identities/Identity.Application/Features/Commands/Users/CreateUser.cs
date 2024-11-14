using AutoMapper;
using Core.Application.Common;
using Core.Application.CQRS;
using Identity.Application.Exceptions;
using Identity.Domain.Interfaces;
using Identity.Domain.Model;

namespace Identity.Application.Features.Commands.Users
{
    public class CreateUserCommand : ICommand<BaseResult>
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string? PhoneNumber { get; set; } = default!;

        public long RoleId { get; set; }

    }


    public class CreateUserHandler : ICommandHandler<CreateUserCommand, BaseResult>
    {

        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;
        public CreateUserHandler(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUnitOfWork = identityUnitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityUnitOfWork.UserRepository.FindByQuery(u => u.Email == request.Email);

            if (user.Any())
            {
                throw new ExistException("User email existed");
            }

            var userCreated = _mapper.Map<User>(request);
            userCreated.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            await _identityUnitOfWork.UserRepository.Add(userCreated);
            await _identityUnitOfWork.SaveChangesAsync();

            return BaseResult.Success();
        }
    }
}

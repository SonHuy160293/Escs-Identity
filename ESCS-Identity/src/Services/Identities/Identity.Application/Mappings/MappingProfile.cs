using AutoMapper;
using Identity.Application.Features.Commands.EmailServiceConfig;
using Identity.Application.Features.Commands.Roles;
using Identity.Application.Features.Commands.Services;
using Identity.Application.Features.Commands.Tokens;
using Identity.Application.Features.Commands.Users;
using Identity.Domain.Dtos;
using Identity.Domain.Model;

namespace Identity.Application.Mappings
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<CreateUserCommand, User>().ReverseMap();
            CreateMap<UserGetDto, User>().ReverseMap();

            CreateMap<CreateServiceCommand, Service>().ReverseMap();
            CreateMap<ServiceGetDto, Service>().ReverseMap();

            CreateMap<CreateEmailServiceConfigCommand, EmailServiceConfig>().ReverseMap();
            CreateMap<EmailServiceConfigGetDto, EmailServiceConfig>().ReverseMap();

            CreateMap<CreateRoleCommand, Role>().ReverseMap();
            CreateMap<RoleGetDto, Role>().ReverseMap();

            CreateMap<CreateUserApiKeyCommand, UserToken>().ReverseMap();
            CreateMap<UserTokenGetDto, UserToken>().ReverseMap();
        }
    }
}

using Application.Features.Account.Commands;
using Application.Features.Account.Commands.Register;
using Common;

namespace Application.Common.Interfaces;

public interface IIdentityService
{
    Task<LoginResponse> Login(LoginViewModel model);
    Task<UserDto> Register(string userName, string email, string password);
}
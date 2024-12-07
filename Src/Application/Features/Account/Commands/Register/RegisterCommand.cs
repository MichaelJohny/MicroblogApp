using Application.Common.Interfaces;
using MediatR;

namespace Application.Features.Account.Commands.Register;

public class RegisterCommand : IRequest<UserDto> 
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserDto>
{
    private readonly IIdentityService _identityService;

    public RegisterCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.Register(request.UserName, request.Email, request.Password);
    }
}
using Application.Common.Interfaces;
using Common;
using MediatR;

namespace Application.Features.Account.Commands.Login;

public class LoginCommand : IRequest<LoginResponse>
{
    public LoginViewModel LoginViewModel { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private readonly IIdentityService _identityService;

    public LoginCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.Login(request.LoginViewModel);
    }
}

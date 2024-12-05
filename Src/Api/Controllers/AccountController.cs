using Application.Features.Account.Commands;
using Application.Features.Account.Commands.Login;
using Application.Features.Account.Commands.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class AccountController : BaseController
{

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
        => Ok(await Mediator.Send(command));
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        => Ok(await Mediator.Send(command));

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> TestAuth()
        => Ok("Authenticated success");
}
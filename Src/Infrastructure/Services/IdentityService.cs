using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Features.Account.Commands;
using Application.Features.Account.Commands.Register;
using Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace MicrblogApp.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;

    public IdentityService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<LoginResponse> Login(LoginViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
            throw new UnauthorizedException("Invalid email or password");

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: true);
        if (!result.Succeeded)
            throw new UnauthorizedException("Invalid email or password");

        var token = _jwtTokenService.GenerateToken(user);
        await _userManager.UpdateAsync(user);
        return new LoginResponse(user.Id, token, user.UserName);
    }

    public async Task<UserDto> Register(string userName, string email, string password)
    {
        var user = new ApplicationUser()
        {
            UserName = userName,
            Email = email,
        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new ValidationException(result.Errors);


        return new UserDto()
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }
}
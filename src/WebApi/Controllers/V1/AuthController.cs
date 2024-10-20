﻿using Application.Auth;
using Application.Common;
using Application.Services;
using Domain.Aggregates;
using Domain.ValueObjects;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.JsonWebTokens;
using WebApi.Hubs;

namespace WebApi.Controllers.V1;

/// <summary>
/// Controller for authentication actions
/// </summary>
[Authorize]
public sealed class AuthController(
    ILogger<ApiController> logger,
    IAppDbContext dbContext,
    IMediator mediator,
    JwtGenerator jwtGenerator,
    IHttpContextAccessor httpContext,
    IHubContext<QrLoginHub> hub) : ApiController(logger)
{
    /// <summary>
    /// Gets the user's claims
    /// </summary>
    [HttpGet("claims")]
    public ActionResult<Dictionary<string, string>> GetUserClaims() =>
        Ok(User.Claims.ToDictionary(c => c.Type, c => c.Value));

    /// <summary>
    /// Logs the user in
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(LoginCommand command, CancellationToken ct)
    {
        var response = await mediator.Send(command, ct);

        switch (response)
        {
            case LoginResponse.Success { Token: var token, User: var user }:
                Response.Cookies.Append("authorization", token);
                return Ok(user);
            // case LoginResponse.TwoFactorRequired:
            //     return Redirect()
            case LoginResponse.Failure:
                return BadRequest("bad credentials");
            default:
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// adds new user
    /// </summary>
    [AllowAnonymous]
    [HttpPost("new_user")]
    public async Task<ActionResult<User>> Login(string username, string password, CancellationToken ct)
    {
        var user = new User(Ulid.NewUlid())
        {
            FullName = username,
            Email = username + "@gmail.com",
            PasswordHash = BC.EnhancedHashPassword(password),
            DateOfBirth = DateTime.Now.AddYears(-20),
            EmploymentStatus = new EmploymentStatus.SelfEmployed(true),
            SavingGoals = [],
            PreferredCurrency = (Currency)"USD",
            Accounts = [],
        };

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync(ct);

        return user;
    }

    [AllowAnonymous]
    [HttpPost("QrLogin")]
    public async Task<ActionResult<User>> LoginWithQr()
    {
        var userId = User.Claims.First(x => x.Type == "sid").Value;

        var user = await dbContext.Users.FindAsync(userId);

        var token = jwtGenerator.GenerateToken(user.GetClaims());

        await hub.Clients.All.SendAsync("GuidCheck", token);

        return Ok();
    }
}
using Application.Features.Posts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class PostsController : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromForm] CreatePostCommand command)
        => Ok(await Mediator.Send(command));
}
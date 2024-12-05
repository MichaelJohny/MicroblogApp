using Application.Features.Posts.Commands;
using Application.Features.Posts.Commands.CreatePost;
using Application.Features.Posts.Commands.UploadImage;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class PostsController : BaseController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreatePostCommand command)
        => Ok(await Mediator.Send(command));
    
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromForm] UploadImageCommand command)
        => Ok(await Mediator.Send(command));
    
}
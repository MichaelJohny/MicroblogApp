using Application.Features.Posts.Commands.CreatePost;
using Application.Features.Posts.Commands.UploadImage;
using Application.Features.Posts.Queries.GetPosta;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class PostsController : BaseController
{
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] CreatePostCommand command)
        => Ok(await Mediator.Send(command));
    
    [HttpPost("upload")]
    [Authorize]
    public async Task<IActionResult> Upload([FromForm] UploadImageCommand command)
        => Ok(await Mediator.Send(command));
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get( )
        => Ok(await Mediator.Send(new GetPostsQuery()));
    
    
}
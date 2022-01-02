using DevGames.API.Models;

using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers;

[Route("api/boards/{id}/posts")]
[ApiController]
public class PostsController : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll(int id)
    {
        return Ok();
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetById(int id, int postId)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post(int id , [FromBody] AddPostInputModel inputModel)
    {
        return Created(nameof(GetById), new { id = id, postId = inputModel.Id });
    }


    [HttpPost("{postId}/comments")]
    public async Task<IActionResult> PostComment(int id, int postId, [FromBody] AddCommentInputModel inputModel)
    {
        return NoContent();
    }
}

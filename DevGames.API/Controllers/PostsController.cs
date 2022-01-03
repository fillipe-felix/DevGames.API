using AutoMapper;

using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;

using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers;

[Route("api/boards/{id}/posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly DevGamesContext _context;
    private readonly IMapper _mapper;

    public PostsController(DevGamesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int id)
    {
        var board = _context.Boards.SingleOrDefault(b => b.Id == id);

        if (board is null)
        {
            return NotFound();
        }
        
        return Ok(board.Posts);
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetById(int id, int postId)
    {
        var board = _context.Boards.SingleOrDefault(b => b.Id == id);

        if (board is null)
        {
            return NotFound();
        }

        var post = board.Posts.SingleOrDefault(p => p.Id == postId);

        if (post is null)
        {
            return NotFound();
        }
        
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Post(int id , [FromBody] AddPostInputModel inputModel)
    {
        var board = _context.Boards.SingleOrDefault(b => b.Id == id);

        if (board is null)
        {
            return NotFound();
        }

        var post = _mapper.Map<Post>(inputModel);
        //var post = new Post(inputModel.Id, inputModel.Title, inputModel.Description);
        
        board.AddPost(post);
        
        return CreatedAtAction(nameof(GetById), new { id = id, postId = post.Id }, inputModel);
    }


    [HttpPost("{postId}/comments")]
    public async Task<IActionResult> PostComment(int id, int postId, [FromBody] AddCommentInputModel inputModel)
    {
        var board = _context.Boards.SingleOrDefault(b => b.Id == id);

        if (board is null)
        {
            return NotFound();
        }

        var post = board.Posts.SingleOrDefault(p => p.Id == postId);

        if (post is null)
        {
            return NotFound();
        }

        var comment = _mapper.Map<Comment>(inputModel);
        //var comment = new Comment(inputModel.Title, inputModel.Description, inputModel.User);
        
        post.AddComment(comment);
        
        return NoContent();
    }
}

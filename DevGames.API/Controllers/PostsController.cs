using AutoMapper;

using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    public IActionResult GetAll(int id)
    {
        var posts = _context.Posts.Where(p => p.BoardId == id);

        if (posts is null)
        {
            return NotFound();
        }
        
        return Ok(posts);
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetById(int id, int postId)
    {
        var post = await _context
            .Posts
            .Include(p => p.Comments)
            .SingleOrDefaultAsync(p => p.Id == postId);
        
        if (post is null)
        {
            return NotFound();
        }
        
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> Post(int id , [FromBody] AddPostInputModel inputModel)
    {
        var post = _mapper.Map<Post>(inputModel);
        post.SetBoardId(id);
        //var post = new Post(inputModel.Title, inputModel.Description, id, inputModel.User);

        _context.Posts.Add(post);
        
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = id, postId = post.Id }, post);
    }


    [HttpPost("{postId}/comments")]
    public async Task<IActionResult> PostComment(int id, int postId, [FromBody] AddCommentInputModel inputModel)
    {
        var postExists = await _context.Posts.AnyAsync(p => p.Id == postId);

        if (!postExists)
        {
            return NotFound();
        }

        var comment = _mapper.Map<Comment>(inputModel);
        comment.SetPostId(postId);
        //var comment = new Comment(inputModel.Title, inputModel.Description, inputModel.User);
        
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}

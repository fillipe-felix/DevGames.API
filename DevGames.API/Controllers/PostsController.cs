using AutoMapper;

using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers;

[Route("api/boards/{id}/posts")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostsController( IMapper mapper, IPostRepository postRepository)
    {
        _mapper = mapper;
        _postRepository = postRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(int id)
    {
        var posts = await _postRepository.GetAllByBoardAsync(id);

        if (posts is null)
        {
            return NotFound();
        }
        
        return Ok(posts);
    }

    [HttpGet("{postId}")]
    public async Task<IActionResult> GetById(int id, int postId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        
        if (post is null)
        {
            return NotFound();
        }
        
        return Ok(post);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(int id , [FromBody] AddPostInputModel inputModel)
    {
        var post = _mapper.Map<Post>(inputModel);
        post.SetBoardId(id);
        //var post = new Post(inputModel.Title, inputModel.Description, id, inputModel.User);

        await _postRepository.AddAsync(post);
        
        return CreatedAtAction(nameof(GetById), new { id = id, postId = post.Id }, post);
    }


    [HttpPost("{postId}/comments")]
    public async Task<IActionResult> PostCommentAsync(int id, int postId, [FromBody] AddCommentInputModel inputModel)
    {
        var postExists = await _postRepository.GetPostByIdAsync(postId);

        if (postExists == null)
        {
            return NotFound();
        }

        var comment = _mapper.Map<Comment>(inputModel);
        comment.SetPostId(postId);
        //var comment = new Comment(inputModel.Title, inputModel.Description, inputModel.User);

        await _postRepository.AddCommentAsync(comment);
        
        return NoContent();
    }
}

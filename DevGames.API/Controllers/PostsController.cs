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

    /// <summary>
    /// Retorna uma lista de posts
    /// </summary>
    /// <param name="id">Id do Board</param>
    /// <returns>List Posts</returns>
    /// <response code="200">Sucess</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Post>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync(int id)
    {
        var posts = await _postRepository.GetAllByBoardAsync(id);

        if (posts is null)
        {
            return NotFound();
        }
        
        return Ok(posts);
    }

    /// <summary>
    /// Busca um post pelo id
    /// </summary>
    /// <param name="id">Id Board</param>
    /// <param name="postId">Id do Post</param>
    /// <returns>Post object</returns>
    [HttpGet("{postId}")]
    [ProducesResponseType(typeof(Post), StatusCodes.Status200OK)]
    [ProducesResponseType( StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id, int postId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        
        if (post is null)
        {
            return NotFound();
        }
        
        return Ok(post);
    }

    /// <summary>
    /// Cria um novo post
    /// </summary>
    /// <remarks>
    /// Request Body example:
    /// 
    ///     {
    ///         "title": "Preciso de ajuda",
    ///         "description": "Preciso de ajuda para finalizar uma corrida",
    ///         "user": "Fillipe"
    ///     }
    /// </remarks>
    /// <param name="id">Id do Board</param>
    /// <param name="inputModel">InputModel object</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(Post), StatusCodes.Status201Created)]
    public async Task<IActionResult> PostAsync(int id , [FromBody] AddPostInputModel inputModel)
    {
        var post = _mapper.Map<Post>(inputModel);
        post.SetBoardId(id);
        //var post = new Post(inputModel.Title, inputModel.Description, id, inputModel.User);

        await _postRepository.AddAsync(post);
        
        return CreatedAtAction(nameof(GetById), new { id = id, postId = post.Id }, post);
    }


    /// <summary>
    /// Cria um comentario no post
    /// </summary>
    /// <remarks>
    /// Request Body example:
    ///
    ///     {
    ///         "title": "Posso te ajuda",
    ///         "description": "Consigo te ajudar, qual é a corrida?",
    ///         "user": "João"
    ///     }
    /// </remarks>
    /// <param name="id">Id do Board</param>
    /// <param name="postId">Id do Post</param>
    /// <param name="inputModel">InputModel object</param>
    /// <returns></returns>
    [HttpPost("{postId}/comments")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

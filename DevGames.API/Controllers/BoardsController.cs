using AutoMapper;

using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence.Repositories;

using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardsController : ControllerBase
{
    private readonly IBoardRepository _boardRepository;
    private readonly IMapper _mapper;

    public BoardsController(IMapper mapper, IBoardRepository boardRepository)
    {
        _mapper = mapper;
        _boardRepository = boardRepository;
    }

    /// <summary>
    /// Retorna uma lisa de boards
    /// </summary>
    /// <returns>List Boards</returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Board>),StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var boards = await _boardRepository.GetAllAsync();
        return Ok(boards);
    }

    /// <summary>
    /// Busca um Board por ID
    /// </summary>
    /// <param name="id">Board Id</param>
    /// <returns>Board Object</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Board), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetById(int id)
    {
        var board = await _boardRepository.GetBoardByIdAsync(id);

        if (board is null)
        {
            return NotFound();
        }

        return Ok(board);
    }

    /// <summary>
    /// Realiza o cadastro de um board
    /// </summary>
    /// <remarks>
    /// Request Body Example:
    /// 
    ///     {
    ///         "gameTitle": "The Crew 2",
    ///         "description": "Jogo de corrida",
    ///         "rules": "não a regras"
    ///     }
    /// </remarks>
    /// <param name="inputModel">Board data</param>
    /// <returns>Created object</returns>
    /// <response code="201">Created</response>
    /// <response code="400">Invalid data</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostAsync([FromBody] AddBoardInputModel inputModel)
    {
        var board = _mapper.Map<Board>(inputModel);
        //var board = new Board(inputModel.id, inputModel.GameTitle, inputModel.Description, inputModel.Rules);

        await _boardRepository.AddAsync(board);
        
        return CreatedAtAction(nameof(GetById), new { id = board.Id }, inputModel);
    }

    /// <summary>
    /// Realiza o update em um board
    /// </summary>
    /// <param name="id">Id do board</param>
    /// <param name="inputModel">Boar data</param>
    /// <returns>No content</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutAsync(int id, [FromBody] UpdateBoardInputModel inputModel)
    {
        var board = await _boardRepository.GetBoardByIdAsync(id);

        if (board is null)
        {
            return NotFound();
        }
        board.Update(inputModel.Description, inputModel.Rules);
        await _boardRepository.UpdateAsync(board);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return NoContent();
    }
}

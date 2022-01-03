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

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var boards = await _boardRepository.GetAllAsync();
        return Ok(boards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var board = await _boardRepository.GetBoardByIdAsync(id);

        if (board is null)
        {
            return NotFound();
        }

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] AddBoardInputModel inputModel)
    {
        var board = _mapper.Map<Board>(inputModel);
        //var board = new Board(inputModel.id, inputModel.GameTitle, inputModel.Description, inputModel.Rules);

        await _boardRepository.AddAsync(board);
        
        return CreatedAtAction(nameof(GetById), new { id = board.Id }, inputModel);
    }

    [HttpPut("{id}")]
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

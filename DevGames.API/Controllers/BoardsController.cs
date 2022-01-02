using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;

using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardsController : ControllerBase
{
    private readonly DevGamesContext _context;

    public BoardsController(DevGamesContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_context.Boards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var board = _context.Boards.SingleOrDefault(b => b.Id == id);

        if (board is null)
        {
            return NotFound();
        }

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddBoardInputModel inputModel)
    {
        var board = new Board(inputModel.id, inputModel.GameTitle, inputModel.Description, inputModel.Rules);

        _context.Boards.Add(board);
        
        return CreatedAtAction(nameof(GetById), new { id = inputModel.id }, inputModel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateBoardInputModel inputModel)
    {
        var board = _context.Boards.SingleOrDefault(b => b.Id == id);

        if (board is null)
        {
            return NotFound();
        }
        
        board.Update(inputModel.Description, inputModel.Rules);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return NoContent();
    }
}

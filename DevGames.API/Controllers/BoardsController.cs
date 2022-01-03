using AutoMapper;

using DevGames.API.Entities;
using DevGames.API.Models;
using DevGames.API.Persistence;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardsController : ControllerBase
{
    private readonly DevGamesContext _context;
    private readonly IMapper _mapper;

    public BoardsController(DevGamesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Boards);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var board = await _context.Boards.SingleOrDefaultAsync(b => b.Id == id);

        if (board is null)
        {
            return NotFound();
        }

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddBoardInputModel inputModel)
    {
        var board = _mapper.Map<Board>(inputModel);
        //var board = new Board(inputModel.id, inputModel.GameTitle, inputModel.Description, inputModel.Rules);

        _context.Boards.Add(board);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetById), new { id = board.Id }, inputModel);
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
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return NoContent();
    }
}

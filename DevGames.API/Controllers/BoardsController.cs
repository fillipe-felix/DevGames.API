using DevGames.API.Models;

using Microsoft.AspNetCore.Mvc;

namespace DevGames.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BoardsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AddBoardInputModel inputModel)
    {
        return Created(nameof(GetById), new { id = inputModel.id });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] UpdateBoardInputModel inputModel)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return NoContent();
    }
}

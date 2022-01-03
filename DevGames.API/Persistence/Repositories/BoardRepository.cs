using DevGames.API.Entities;

using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Persistence.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly DevGamesContext _context;

    public BoardRepository(DevGamesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Board>> GetAllAsync()
    {
        return await _context
            .Boards
            .Include(p => p.Posts)
            .ToListAsync();
    }

    public async Task<Board> GetBoardByIdAsync(int id)
    {
        return await _context
            .Boards
            .Include(b => b.Posts)
            .SingleOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddAsync(Board board)
    {
        await _context.Boards.AddAsync(board);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Board board)
    {
        _context.Boards.Update(board);
        await _context.SaveChangesAsync();
    }
}

using DevGames.API.Entities;

namespace DevGames.API.Persistence.Repositories;

public interface IBoardRepository
{
    Task<IEnumerable<Board>> GetAllAsync();
    Task<Board> GetBoardByIdAsync(int id);
    Task AddAsync(Board board);
    Task UpdateAsync(Board board);
}

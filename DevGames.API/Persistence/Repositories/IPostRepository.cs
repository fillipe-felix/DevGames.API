using DevGames.API.Entities;

namespace DevGames.API.Persistence.Repositories;

public interface IPostRepository
{
    Task<IEnumerable<Post>> GetAllByBoardAsync(int boardId);
    Task<Post> GetPostByIdAsync(int id);
    Task AddAsync(Post post);
    Task AddCommentAsync(Comment comment);
}

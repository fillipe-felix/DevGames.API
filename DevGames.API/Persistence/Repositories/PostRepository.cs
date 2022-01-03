using DevGames.API.Entities;

using Microsoft.EntityFrameworkCore;

namespace DevGames.API.Persistence.Repositories;

public class PostRepository : IPostRepository
{
    private readonly DevGamesContext _context;

    public PostRepository(DevGamesContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetAllByBoardAsync(int boardId)
    {
        var posts = await _context
            .Posts
            .Include(p => p.Comments)
            .Where(p => p.BoardId == boardId).ToListAsync();

        return posts;
    }

    public async Task<Post> GetPostByIdAsync(int id)
    {
        var post = await _context
            .Posts
            .Include(p => p.Comments)
            .SingleOrDefaultAsync(p => p.Id == id);

        return post;
    }

    public async Task AddAsync(Post post)
    {
        await _context.Posts.AddAsync(post);
        
        await _context.SaveChangesAsync();
    }

    public async Task AddCommentAsync(Comment comment)
    {
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
    }
}

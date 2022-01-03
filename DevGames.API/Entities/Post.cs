namespace DevGames.API.Entities;

public class Post
{
    public Post(string title, string description, string user)
    {
        Title = title;
        Description = description;
        User = user;

        CreatedAt = DateTime.Now;
        Comments = new List<Comment>();
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string User { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public List<Comment> Comments { get; private set; }
    public int BoardId { get; private set; }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public void SetBoardId(int boardId)
    {
        BoardId = boardId;
    }
}

namespace DevGames.API.Entities;

public class Comment
{
    public Comment(string title, string description, string user)
    {
        Title = title;
        Description = description;
        User = user;

        CreatedAt = DateTime.Now;
    }

    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string User { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int PostId { get; private set; }

    public void SetPostId(int postId)
    {
        PostId = postId;
    }
}

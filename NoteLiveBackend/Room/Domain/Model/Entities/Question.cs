namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class Question
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public string Text { get; private set; }
    public int Likes { get; private set; }

    public Question(Guid id, Guid userId, string text)
    {
        Id = id;
        UserId = userId;
        Text = text;
        Likes = 0;
    }

    public void Like()
    {
        Likes++;
    }
}

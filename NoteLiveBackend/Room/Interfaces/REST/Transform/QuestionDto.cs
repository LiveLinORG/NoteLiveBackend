namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public class QuestionDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public int Likes { get; set; }
    public string UserName { get; set; }

    public QuestionDto(Guid id, string text, int likes, string userName)
    {
        Id = id;
        Text = text;
        Likes = likes;
        UserName = userName;
    }
}

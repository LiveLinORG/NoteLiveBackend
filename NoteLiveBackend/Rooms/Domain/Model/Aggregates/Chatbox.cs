namespace NoteLiveBackend.Rooms.Domain.Model.Aggregates;

public partial class Chatbox
{
    public int Id { get; }
    public string Content { get; private set; }

    public Chatbox(string content)
    {
        Content = content;
    }
}
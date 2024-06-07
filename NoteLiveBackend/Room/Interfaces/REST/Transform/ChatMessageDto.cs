namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public class ChatMessageDto
{
    public string Content { get; set; }
    public string UserName { get; set; }

    public ChatMessageDto(string content, string userName)
    {
        Content = content;
        UserName = userName;
    }
}

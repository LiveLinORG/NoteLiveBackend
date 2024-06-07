namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class Chat
{
    public Guid RoomId { get; private set; }
    public List<ChatMessage> Messages { get; private set; }
    public List<Guid> UserIds { get; private set; }

    public Chat(Guid roomId)
    {
        RoomId = roomId;
        Messages = new List<ChatMessage>();
        UserIds = new List<Guid>();
    }

    public void AddMessage(ChatMessage message)
    {
        Messages.Add(message);
    }
}

public class ChatMessage
{
    public string Content { get; private set; }
    public string UserName { get; private set; }

    public ChatMessage(string content, string userName)
    {
        Content = content;
        UserName = userName;
    }
}

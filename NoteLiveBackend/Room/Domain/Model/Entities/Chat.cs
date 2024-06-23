using System.ComponentModel.DataAnnotations.Schema;

namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class Chat
{
    public Guid Id { get; private set; }
    public Guid RoomId { get; private set; }
    public List<ChatMessage> Messages { get; private set; }
        
    [NotMapped]
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
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public Guid UserId { get; private set; }

    public ChatMessage(string content, Guid userId)
    {
        Content = content;
        UserId = userId;
    }
}
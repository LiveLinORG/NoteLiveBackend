using System.ComponentModel.DataAnnotations.Schema;

namespace NoteLiveBackend.Room.Domain.Model.Entities;
public class Chat
{
    public Guid Id { get; private set; }

    public bool isActivated { get; set; }

    public Chat(Guid roomId)
    {
        Room.ChatId = roomId;
        isActivated = true;
    }
    // Relación con Room
    public Room Room { get; set; }
    private Chat(){}
}

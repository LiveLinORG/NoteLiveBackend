using System.ComponentModel.DataAnnotations.Schema;

namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class Question
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid RoomId { get; private set; } // Agrega la propiedad RoomId
    public string Text { get; private set; }
    public int Likes { get; private set; }

    public User User { get; set; } // Agrega la propiedad de navegación User
    public Room Room { get; set; } // Propiedad de navegación con la entidad Room


    public Question(Guid id, Guid userId, Guid roomId, string text) // Modifica el constructor para incluir RoomId
    {
        Id = id;
        UserId = userId;
        RoomId = roomId;
        Text = text;
        Likes = 0;
    }

    public void Like()
    {
        Likes++;
    }
}

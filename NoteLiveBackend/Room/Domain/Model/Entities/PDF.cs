namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class PDF
{
    public Guid Id { get; private set; }
    public byte[] Content { get; private set; }
    public Guid RoomId { get; private set; } // Agrega la propiedad RoomId

    public PDF(Guid id, byte[] content, Guid roomId) // Modifica el constructor para incluir RoomId
    {
        Id = id;
        Content = content;
        RoomId = roomId;
    }
}
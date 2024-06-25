namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class PDF
{
    public Guid Id { get; private set; }
    public byte[] Content { get; private set; }
    public Guid RoomId { get; private set; }

    public PDF(byte[] content, Room _room)
    {
        Id = Guid.NewGuid();
        Content = content;
        RoomId = _room.Id;
    }
    private PDF() { }
    public Room Room { get; set; }
}
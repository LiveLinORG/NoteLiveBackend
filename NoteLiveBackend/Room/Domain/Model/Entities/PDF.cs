using System.ComponentModel.DataAnnotations.Schema;

namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class PDF
{
    public Guid Id { get; private set; }
    public byte[]? Content { get; private set; }
    [NotMapped]
    public Guid RoomId { get; private set; }

    public PDF(byte[] content, Guid _RoomId)
    {
        Id = Guid.NewGuid();
        Content = content;
        RoomId = _RoomId;
    }
    public PDF(Guid _RoomId)
    {
        Id = Guid.NewGuid();
        RoomId = _RoomId;
    }
    private PDF() { }
}
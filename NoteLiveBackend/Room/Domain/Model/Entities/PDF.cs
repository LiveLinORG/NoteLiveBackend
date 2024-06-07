namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class PDF
{
    public Guid Id { get; private set; }
    public byte[] Content { get; private set; }

    public PDF(Guid id, byte[] content)
    {
        Id = id;
        Content = content;
    }
}

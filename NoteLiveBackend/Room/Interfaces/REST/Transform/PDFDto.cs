namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public class PDFDto
{
    public Guid Id { get; set; }
    public byte[] Content { get; set; }

    public PDFDto(Guid id, byte[] content)
    {
        Id = id;
        Content = content;
    }
}

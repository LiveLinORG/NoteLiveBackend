namespace NoteLiveBackend.Room.Domain.Model.Queries;

public class GetPDFDetailsQuery
{
    public Guid RoomId { get; }

    public GetPDFDetailsQuery(Guid roomId)
    {
        RoomId = roomId;
    }
}
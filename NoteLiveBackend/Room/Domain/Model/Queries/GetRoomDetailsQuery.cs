namespace NoteLiveBackend.Room.Domain.Model.Queries;

public class GetRoomDetailsQuery
{
    public Guid RoomId { get; }

    public GetRoomDetailsQuery(Guid roomId)
    {
        RoomId = roomId;
    }
}

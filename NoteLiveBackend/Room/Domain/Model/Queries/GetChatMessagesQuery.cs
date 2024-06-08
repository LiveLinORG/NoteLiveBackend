namespace NoteLiveBackend.Room.Domain.Model.Queries;

public class GetChatMessagesQuery
{
    public Guid RoomId { get; }

    public GetChatMessagesQuery(Guid roomId)
    {
        RoomId = roomId;
    }
}
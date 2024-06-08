namespace NoteLiveBackend.Room.Domain.Model.Commands;

public class JoinRoomCommand
{
    public Guid RoomId { get; }
    public Guid UserId { get; }

    public JoinRoomCommand(Guid roomId, Guid userId)
    {
        RoomId = roomId;
        UserId = userId;
    }
}

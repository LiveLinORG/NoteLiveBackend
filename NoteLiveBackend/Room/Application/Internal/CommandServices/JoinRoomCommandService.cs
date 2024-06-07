namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class JoinRoomCommandService
{
    private readonly IRoomRepository _roomRepository;

    public JoinRoomCommandService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void Handle(JoinRoomCommand command)
    {
        var room = _roomRepository.GetById(command.RoomId);
        if (room == null) throw new RoomNotFoundException();

        room.AddUser(command.UserId);
        _roomRepository.Update(room);
    }
}

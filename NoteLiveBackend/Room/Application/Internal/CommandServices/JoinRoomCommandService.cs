using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Room.Domain.Model.Commands;

namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class JoinRoomCommandService
{
    private readonly IRoomRepository _roomRepository;

    public JoinRoomCommandService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task Handle(JoinRoomCommand command)
    {
        var room = await _roomRepository.GetById(command.RoomId);
        if (room == null) throw new RoomNotFoundException();

        room.AddUser(command.UserId);
        await _roomRepository.Update(room);
    }
}

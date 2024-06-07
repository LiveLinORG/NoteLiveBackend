using NoteLiveBackend.Room.Domain.Model.Commands;
namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class CreateRoomCommandService
{
    private readonly IRoomRepository _roomRepository;

    public CreateRoomCommandService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void Handle(CreateRoomCommand command)
    {
        var room = new Domain.Model.Entities.Room(command.RoomId, command.RoomName, command.ProfessorId);
        _roomRepository.Add(room);
    }
}
public interface IRoomRepository
{
    Domain.Model.Entities.Room GetById(Guid id);
    IEnumerable<Domain.Model.Entities.Room> GetAll();
    void Add(Domain.Model.Entities.Room room);
    void Update(Domain.Model.Entities.Room room);
    void Remove(Domain.Model.Entities.Room room);
}

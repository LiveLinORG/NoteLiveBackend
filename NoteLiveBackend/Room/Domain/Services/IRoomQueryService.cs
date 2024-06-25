using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Domain.Services;

public interface IRoomQueryService
{
    Task<Model.Entities.Room?> Handle(GetRoomByIdQuery query);

    Task<IEnumerable<Model.Entities.Room>> Handle(GetAllRoomsQuery query);

    Task<IEnumerable<Model.Entities.Room>> Handle(GetRoomsByPDFNameQuery query);
}
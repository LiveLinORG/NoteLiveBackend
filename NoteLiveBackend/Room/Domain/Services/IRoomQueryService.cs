using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Domain.Services;

public interface IRoomQueryService
{
    Task<Model.Entities.Room?> Handle(GetRoomByIdQuery query);

    Task<IEnumerable<Model.Entities.Room>> Handle(GetAllRoomsQuery query);

    Task<IEnumerable<Model.Entities.Room>> Handle(GetRoomsByPDFNameQuery query);
    Task<Model.Entities.Room?> Handle(GetRoomByNameQuery query);
    Task<IEnumerable<User>> Handle(GetUsersByRoomIdQuery query);

}
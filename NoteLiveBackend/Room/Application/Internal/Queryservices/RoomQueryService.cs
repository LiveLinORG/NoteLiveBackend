using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;

namespace NoteLiveBackend.Room.Application.Internal.Queryservices;

public class RoomQueryService(IRoomRepository roomRepository): IRoomQueryService
{
    public async Task<Domain.Model.Entities.Room?> Handle(GetRoomByIdQuery query)
    {
        return await roomRepository.FindByIdAsync(query.RoomId);
    }

    public async Task<IEnumerable<Domain.Model.Entities.Room>> Handle(GetAllRoomsQuery query)
    {
        return await roomRepository.ListAsync();
    }
    public async Task<Domain.Model.Entities.Room?> Handle(GetRoomByNameQuery query)
    {
        var room = await roomRepository.FindByNameAsync(query.RoomName);
        return room;
    }
    public async Task<IEnumerable<Domain.Model.Entities.Room>> Handle(GetRoomsByPDFNameQuery query)
    {
        return await roomRepository.FindByPdfNameAsync(query.Name);
    }
    public async Task<IEnumerable<User>> Handle(GetUsersByRoomIdQuery query)
    {
        return await roomRepository.GetUsersByRoomIdAsync(query.RoomId);
    }
}
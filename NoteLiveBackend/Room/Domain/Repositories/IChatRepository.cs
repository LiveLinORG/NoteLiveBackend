using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Shared.Domain.Repositories;

namespace NoteLiveBackend.Room.Domain.Repositories;

public interface IChatRepository: IBaseRepository<Chat>
{
    Task<Chat?> GetByRoomId(Guid roomId);

}
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Shared.Domain.Repositories;

namespace NoteLiveBackend.Room.Domain.Repositories;

public interface IQuestionRepository : IBaseRepository<Question>
{

    Task<IEnumerable<Question>> GetByRoomId(Guid roomId);

    Task<IEnumerable<Question>> GetQuestionsByRoomId(Guid queryRoomId);
}
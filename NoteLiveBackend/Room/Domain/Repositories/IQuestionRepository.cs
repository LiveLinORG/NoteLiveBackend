using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Domain.Repositories;

public interface IQuestionRepository
{

    Task<IEnumerable<Question>> GetByRoomId(Guid roomId);

    Task<IEnumerable<Question>> GetQuestionsByRoomId(Guid queryRoomId);
}
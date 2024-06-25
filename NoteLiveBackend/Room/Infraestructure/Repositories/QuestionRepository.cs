using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Question>> GetByRoomId(Guid roomId)
    {
        return _context.Questions.Where(q => q.RoomId == roomId).ToList();
    }

    public async Task<IEnumerable<Question>> GetQuestionsByRoomId(Guid queryRoomId)
    {
        return _context.Questions.Where(q => q.RoomId == queryRoomId).ToList();
    }
}

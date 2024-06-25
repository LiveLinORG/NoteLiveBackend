using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{

    public QuestionRepository(AppDbContext context) : base(context) { }



    public async Task<IEnumerable<Question>> GetQuestionsByRoomId(Guid queryRoomId)
    {
        return Context.Questions.Where(q => q.RoomId == queryRoomId).ToList();
    }
    public async Task<IEnumerable<Question>> GetByRoomId(Guid roomId)
    {
        return await Context.Questions.Where(q => q.RoomId == roomId).ToListAsync();
    }
}

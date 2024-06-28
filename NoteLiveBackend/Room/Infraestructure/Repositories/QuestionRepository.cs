using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    /**
     * <summary>
     * Initializes a new instance of the QuestionRepository class.
     * </summary>
     * <param name="context">The database context to be used by the repository.</param>
     */
    public QuestionRepository(AppDbContext context) : base(context) { }


    /**
     * <summary>
     *  Gets question associated with a specific room ID.
     * </summary>
     * <param name="queryRoomId">The unique identifier of the room.</param>
     * <returns>A task</returns>
     */
    public async Task<IEnumerable<Question>> GetQuestionsByRoomId(Guid queryRoomId)
    {
        return Context.Questions.Where(q => q.RoomId == queryRoomId).ToList();
    }
    
    /**
     * <summary>
     *  Gets questions associated with a specific room ID.
     * </summary>
     * <param name="roomId">The id of the room</param>
     * <returns>A task</returns>
     */
    public async Task<IEnumerable<Question>> GetByRoomId(Guid roomId)
    {
        return await Context.Questions.Where(q => q.RoomId == roomId).ToListAsync();
    }
}

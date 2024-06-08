using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public Question GetById(Guid id)
    {
        return _context.Questions.Find(id);
    }

    public void Add(Question question)
    {
        _context.Questions.Add(question);
        _context.SaveChanges();
    }

    public void Update(Question question)
    {
        _context.Questions.Update(question);
        _context.SaveChanges();
    }

    public IEnumerable<Question> GetByRoomId(Guid roomId)
    {
        return _context.Questions.Where(q => q.RoomId == roomId).ToList();
    }

    public IEnumerable<Question> GetQuestionsByRoomId(Guid queryRoomId)
    {
        //falta
        throw new NotImplementedException();
    }

    Task<IEnumerable<Question>> IQuestionRepository.GetByRoomId(Guid roomId)
    {
        //falta
        throw new NotImplementedException();
    }
}

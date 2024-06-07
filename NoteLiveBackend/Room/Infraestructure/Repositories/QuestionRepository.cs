using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly DbContext _context;

    public QuestionRepository(DbContext context)
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
}

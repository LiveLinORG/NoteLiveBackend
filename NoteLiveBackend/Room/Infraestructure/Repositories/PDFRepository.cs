using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class PDFRepository : IPDFRepository
{
    private readonly AppDbContext _context;

    public PDFRepository(AppDbContext context)
    {
        _context = context;
    }

    public PDF GetByRoomId(Guid roomId)
    {
        return _context.PDFs.FirstOrDefault(p => p.RoomId == roomId);
    }

    public IEnumerable<Question> GetQuestionsByRoomId(Guid roomId)
    {
        throw new NotImplementedException();
    }

    public PDF GetPDFDetailsByRoomId(Guid queryRoomId)
    {
        throw new NotImplementedException();
    }

    public void Add(PDF pdf)
    {
        _context.PDFs.Add(pdf);
        _context.SaveChanges();
    }

    public void Update(PDF pdf)
    {
        _context.PDFs.Update(pdf);
        _context.SaveChanges();
    }

    Task<PDF> IPDFRepository.GetByRoomId(Guid roomId)
    {
        throw new NotImplementedException();
    }
}

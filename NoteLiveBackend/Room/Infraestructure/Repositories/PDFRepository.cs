using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class PDFRepository : IPDFRepository
{
    private readonly DbContext _context;

    public PDFRepository(DbContext context)
    {
        _context = context;
    }

    public PDF GetByRoomId(Guid roomId)
    {
        return _context.PDFs.FirstOrDefault(p => p.RoomId == roomId);
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
}

using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Model.Entities.Room> GetById(Guid id)
    {
        var room = _context.Rooms.SingleOrDefault(r => r.Id == id);

        if (room == null)
        {
            throw new RoomNotFoundException();
        }

        return room;
    }


    public IEnumerable<Domain.Model.Entities.Room> GetAll()
    {
        return _context.Rooms.ToList();
    }

    public void Add(Domain.Model.Entities.Room room)
    {
        _context.Rooms.Add(room);
        _context.SaveChanges();
    }

    public async Task Update(Domain.Model.Entities.Room room)
    {
        _context.Rooms.Update(room);
        await _context.SaveChangesAsync(); // Guardar los cambios asincrónicamente
    }

    public void Remove(Domain.Model.Entities.Room room)
    {
        _context.Rooms.Remove(room);
        _context.SaveChanges();
    }
}
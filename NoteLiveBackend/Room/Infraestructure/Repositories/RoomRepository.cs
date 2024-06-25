using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class RoomRepository(AppDbContext _context) : BaseRepository<Domain.Model.Entities.Room>(_context),IRoomRepository
{

    public new async Task<Domain.Model.Entities.Room?> FindByIdAsync(Guid id) =>
        await _context.Set<Domain.Model.Entities.Room>()
            .Include(r => r.Chat)
            .FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IEnumerable<Domain.Model.Entities.Room>> FindByPdfNameAsync(string searchText)
    {
        var rooms = await Context.Set<Domain.Model.Entities.Room>()
            .ToListAsync();

        var filteredRooms = rooms.Where(r => ContainsText(r.PDF.Content, searchText));

        return filteredRooms;
    }
    private bool ContainsText(byte[] content, string searchText)
    {
        string text = System.Text.Encoding.UTF8.GetString(content);
        return text.Contains(searchText);
    }
  
}
using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class RoomRepository(AppDbContext _context,IPDFCommandService _pdfCommandService) : BaseRepository<Domain.Model.Entities.Room>(_context),IRoomRepository
{        
    

    // Find Room by Id
    public new async Task<Domain.Model.Entities.Room?> FindByIdAsync(Guid id) =>
        await _context.Set<Domain.Model.Entities.Room>().FirstOrDefaultAsync(r => r.Id == id);


    // Find Room by Id including Chat
    public async Task<Domain.Model.Entities.Room?> FindByIdWithChatAsync(Guid id) =>
        await _context.Set<Domain.Model.Entities.Room>()
            .Include(r => r.Chat)
            .FirstOrDefaultAsync(r => r.Id == id);

    // Find Room by Id including Chat and PDF
    public async Task<Domain.Model.Entities.Room?> FindByIdWithChatAndPdfAsync(Guid id) =>
        await _context.Set<Domain.Model.Entities.Room>()
            .Include(r => r.Chat)
            .Include(r => r.PDF)
            .FirstOrDefaultAsync(r => r.Id == id);

  

    // Find Room by Id including PDF and Questions
    public async Task<(byte[]?, IReadOnlyList<Question?>)> FindPdfAndQuestionsAsync(Guid id)
    {
        var room = await _context.Set<Domain.Model.Entities.Room>()
            .Include(r => r.Questions)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
        {
            return (null, new List<Question?>().AsReadOnly());
        }

        // Associate PDF if necessary
        room.AssociatePDF(_pdfCommandService);

        // Load PDF byte array
        await _context.Entry(room)
            .Reference(r => r.PDF)
            .LoadAsync();

        // Return PDF byte array and Questions list
        
        return (room.GetPDFContent(), room.Questions);
    }

 
    
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
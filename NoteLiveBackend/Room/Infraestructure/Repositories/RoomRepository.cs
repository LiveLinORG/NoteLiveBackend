using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.IAM.Domain.Model.Aggregates;
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
    
    /**
     * <summary>
     *  Finds a Room entity by its identifier.
     * </summary>
     * <param name="id">The id of the room</param>
     * <returns>A task</returns>
     */
    public new async Task<Domain.Model.Entities.Room?> FindByIdAsync(Guid id) =>
        await _context.Set<Domain.Model.Entities.Room>().FirstOrDefaultAsync(r => r.Id == id);

    /**
     * <summary>
     *  Finds a Room entity by its name.
     * </summary>
     * <param name="roomName">The name of the room</param>
     * <returns>A task</returns>
     */
    public async Task<Domain.Model.Entities.Room?> FindByNameAsync(string roomName)
    {
        return await _context.Rooms.FirstOrDefaultAsync(r => r.Name == roomName);
    }

    /**
     * <summary>
     *  Finds a Room entity by its identifier including its Chat.
     * </summary>
     * <param name="id">The id of the room</param>
     * <returns>A task</returns>
     */
    public async Task<Domain.Model.Entities.Room?> FindByIdWithChatAsync(Guid id) =>
        await _context.Set<Domain.Model.Entities.Room>()
            .Include(r => r.Chat)
            .FirstOrDefaultAsync(r => r.Id == id);

    /**
     * <summary>
     *  Finds a Room entity by its identifier including its Chat and PDF.
     * </summary>
     * <param name="id">The id of the room</param>
     * <returns>A task</returns>
     */
    public async Task<Domain.Model.Entities.Room?> FindByIdWithChatAndPdfAsync(Guid id) =>
        await _context.Set<Domain.Model.Entities.Room>()
            .Include(r => r.Chat)
            .Include(r => r.PDF)
            .FirstOrDefaultAsync(r => r.Id == id);

  

    /**
     * <summary>
     *  Finds a Room entity by its identifier including its questions and PDF.
     * </summary>
     * <param name="id">The id of the room</param>
     * <returns>A task</returns>
     */
    public async Task<(byte[]?, IReadOnlyList<Question?>)> FindPdfAndQuestionsAsync(Guid id)
    {
        var room = await _context.Set<Domain.Model.Entities.Room>()
            .Include(r => r.Questions)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (room == null)
        {
            return (null, new List<Question?>().AsReadOnly());
        }

        await _context.Entry(room)
            .Reference(r => r.PDF)
            .LoadAsync();

        
        return (room.GetPDFContent(), room.Questions);
    }

    /**
    * <summary>
    *  Finds a Room entity by PDF name.
    * </summary>
    * <param name="searchText">The text to search for in PDF content.</param>
    * <returns>A task</returns>
    */
    public async Task<IEnumerable<Domain.Model.Entities.Room>> FindByPdfNameAsync(string searchText)
    {
        var rooms = await Context.Set<Domain.Model.Entities.Room>()
            .ToListAsync();

        var filteredRooms = rooms.Where(r => ContainsText(r.PDF.Content, searchText));

        return filteredRooms;
    }
    
    /**
    * <summary>
    *  Checks if the specified content contains the search text.
    * </summary>
    * <param name="content">The content to search in.</param>
    * <param name="searchText">The text to search for.</param>
    * <returns>True if the content contains the search text; otherwise, false.</returns>
    */
    private bool ContainsText(byte[] content, string searchText)
    {
        string text = System.Text.Encoding.UTF8.GetString(content);
        return text.Contains(searchText);
    }
    
    /**
    * <summary>
    *  Loads the users associated with a Room entity asynchronously.
    * </summary>
    * <param name="room">The Room entity whose users need to be loaded.</param>
    * <returns>A task.</returns>
    */
    public async Task LoadUsersAsync(Domain.Model.Entities.Room room)
    {
        await _context.Entry(room)
            .Collection(r => r.Users)
            .LoadAsync();
    }
    
    /**
    * <summary>
    *  Gets a Room entity with its users
    * </summary>
    * <param name="roomId">The id of the room.</param>
    * <returns>A task.</returns>
    */
    public async Task<Domain.Model.Entities.Room> GetRoomWithUsersAsync(Guid roomId)
    {
        return await _context.Rooms.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == roomId);
    }
    
    /**
    * <summary>
    *  Gets the users associated with a Room entity by Room ID
    * </summary>
    * <param name="roomId">The id of the room.</param>
    * <returns>A task.</returns>
    */
    public async Task<IEnumerable<User>> GetUsersByRoomIdAsync(Guid roomId)
    {
        return await _context.Rooms
            .Where(r => r.Id == roomId)
            .SelectMany(r => r.Users)
            .ToListAsync();
    }
    
    /**
     * <summary>
     *  Saves the changes made to the context
     * </summary>
     * <returns>A task</returns>
     */
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
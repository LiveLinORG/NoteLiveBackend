﻿using Microsoft.EntityFrameworkCore;
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
    

    // Find Room by Id
    public new async Task<Domain.Model.Entities.Room?> FindByIdAsync(Guid id) =>
        await _context.Set<Domain.Model.Entities.Room>().FirstOrDefaultAsync(r => r.Id == id);

    public async Task<Domain.Model.Entities.Room?> FindByNameAsync(string roomName)
    {
        return await _context.Rooms.FirstOrDefaultAsync(r => r.Name == roomName);
    }
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

        await _context.Entry(room)
            .Reference(r => r.PDF)
            .LoadAsync();

        
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
  
    public async Task LoadUsersAsync(Domain.Model.Entities.Room room)
    {
        await _context.Entry(room)
            .Collection(r => r.Users)
            .LoadAsync();
    }
    public async Task<Domain.Model.Entities.Room> GetRoomWithUsersAsync(Guid roomId)
    {
        return await _context.Rooms.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == roomId);
    }
    //usar este?
    public async Task<IEnumerable<User>> GetUsersByRoomIdAsync(Guid roomId)
    {
        return await _context.Rooms
            .Where(r => r.Id == roomId)
            .SelectMany(r => r.Users)
            .ToListAsync();
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
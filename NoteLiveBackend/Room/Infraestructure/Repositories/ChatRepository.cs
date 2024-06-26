using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _context;

    public ChatRepository(AppDbContext context)
    {
        _context = context;
    }
  
    public async Task<Chat?> GetByRoomId(Guid roomId)
    {
        return _context.Chats.FirstOrDefault(c => c.Id == roomId);
    }

    public Task AddSync(Chat entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Chat? chat)
    {
        _context.Chats.Update(chat);
        _context.SaveChanges();
    }
    // Implementación vacía o de excepción para métodos no utilizados
    public Task AddAsync(Chat entity)
    {
        throw new NotImplementedException("AddAsync method is not implemented in ChatRepository.");
    }

    public Task<Chat?> FindByIdAsync(Guid id)
    {
        throw new NotImplementedException("FindByIdAsync method is not implemented in ChatRepository.");
    }

    public Task<IEnumerable<Chat>> ListAsync()
    {
        throw new NotImplementedException("ListAsync method is not implemented in ChatRepository.");
    }

    public void Remove(Chat entity)
    {
        throw new NotImplementedException("Remove method is not implemented in ChatRepository.");
    }

    public async Task UpdateAsync(Chat entity)
    {
        _context.Chats.Update(entity);
        await _context.SaveChangesAsync();
    }
}
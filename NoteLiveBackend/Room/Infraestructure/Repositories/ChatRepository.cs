using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _context;

    public ChatRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<ChatMessage> GetChatMessagesByRoomId(Guid roomId)
    {
        throw new NotImplementedException();
    }

    Task<Chat> IChatRepository.GetByRoomId(Guid roomId)
    {
        throw new NotImplementedException();
    }

    public Chat GetByRoomId(Guid roomId)
    {
        return _context.Chats.FirstOrDefault(c => c.RoomId == roomId);
    }

    public void Add(Chat chat)
    {
        _context.Chats.Add(chat);
        _context.SaveChanges();
    }

    public void Update(Chat chat)
    {
        _context.Chats.Update(chat);
        _context.SaveChanges();
    }
}

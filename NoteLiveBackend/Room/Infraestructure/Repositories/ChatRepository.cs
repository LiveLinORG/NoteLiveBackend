using Microsoft.EntityFrameworkCore;
using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly DbContext _context;

    public ChatRepository(DbContext context)
    {
        _context = context;
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

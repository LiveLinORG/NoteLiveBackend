using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Application.Internal.Queryservices;

public interface IChatRepository
{
    IEnumerable<ChatMessage> GetChatMessagesByRoomId(Guid roomId);
    Task<Chat?> GetByRoomId(Guid roomId);
}

public class GetChatMessagesQueryService
{
    private readonly IChatRepository _chatRepository;

    public GetChatMessagesQueryService(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }

    public IEnumerable<ChatMessage> Handle(GetChatMessagesQuery query)
    {
        // Obtener los mensajes del chat según el roomId
        return _chatRepository.GetChatMessagesByRoomId(query.RoomId);
    }
}
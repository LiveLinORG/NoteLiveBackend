using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;

namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class ChatCommandService : IChatCommandService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IChatRepository _chatRepository;

    public ChatCommandService(IRoomRepository roomRepository, IChatRepository chatRepository)
    {
        _roomRepository = roomRepository;
        _chatRepository = chatRepository;
    }

}
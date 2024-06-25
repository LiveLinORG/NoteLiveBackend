using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Application.Internal.Queryservices;
public class PDFQueryService : IPDFQueryService
{
    private readonly IRoomRepository _roomRepository;

    public PDFQueryService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task<Domain.Model.Entities.Room?> Handle(GetPDFWithQuestionsByRoomIdQuery query)
    {
        return await _roomRepository.FindByIdWithPdfAndQuestionsAsync(query.RoomId);
    }
}
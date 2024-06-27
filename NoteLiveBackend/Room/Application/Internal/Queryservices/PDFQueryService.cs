using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Application.Internal.Queryservices;
public class PDFQueryService : IPDFQueryService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IPDFRepository _pdfRepository;
    public PDFQueryService(IRoomRepository roomRepository,IPDFRepository pdfRepository)
    {
        _roomRepository = roomRepository;
        _pdfRepository = pdfRepository;
    }

    public async Task<(byte[]?, IReadOnlyList<Question?>)> Handle(GetPDFWithQuestionsByRoomIdQuery query)
    {
        return await _roomRepository.FindPdfAndQuestionsAsync(query.RoomId);
    }

    public async Task<PDF?> Handle(GetPDFByIdQuery query)
    {
        return await _pdfRepository.GetPDFByIdAsync(query.id);

    }

}
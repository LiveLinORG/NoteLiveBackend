using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Domain.Services;

public interface  IPDFQueryService
{
    Task<(byte[]?, IReadOnlyList<Question?>)> Handle(GetPDFWithQuestionsByRoomIdQuery query);
    Task<PDF?> Handle(GetPDFByIdQuery query);

}
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Domain.Services;

public interface  IPDFQueryService
{
    Task<Model.Entities.Room?> Handle(GetPDFWithQuestionsByRoomIdQuery query);

}
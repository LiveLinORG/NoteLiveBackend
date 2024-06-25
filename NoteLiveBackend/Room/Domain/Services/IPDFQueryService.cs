using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Domain.Services;

public interface  IPDFQueryService
{
    Task<(byte[]?, List<Question>?)> Handle(GetPDFWithQuestionsByRoomIdQuery query);

}
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Domain.Services;

public interface IQuestionQueryService
{
    Task<IEnumerable<Question>> Handle(GetQuestionsByRoomIdQuery query);
    Task<Question?> Handle(GetQuestionByIdQuery query);

    Task<IEnumerable<Question>> Handle(GetQuestionsByRoomQuery query);
}
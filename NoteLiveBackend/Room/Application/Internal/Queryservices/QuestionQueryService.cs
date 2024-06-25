using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Shared.Domain.Repositories;

namespace NoteLiveBackend.Room.Application.Internal.Queryservices;

public class QuestionQueryService(IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
    : IQuestionQueryService
{


    public async Task<IEnumerable<Question>> Handle(GetQuestionsByRoomIdQuery query)
    {
        return await questionRepository.GetByRoomId(query.RoomId);
    }

    public async Task<Question?> Handle(GetQuestionByIdQuery query)
    {
        return await questionRepository.FindByIdAsync(query.QuestionId);
    }


}
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;

namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class QuestionCommandService : IQuestionCommandService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionCommandService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<Guid> Handle(CreateQuestionCommand command)
    {
        var question = new Question(command.UserId, command.RoomId, command.Text);
        await _questionRepository.AddAsync(question);
        return question.Id;
    }
}
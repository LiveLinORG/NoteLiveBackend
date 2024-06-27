using Microsoft.AspNetCore.Http.HttpResults;
using NoteLiveBackend.IAM.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;

namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class QuestionCommandService : IQuestionCommandService
{
    private readonly IQuestionRepository _questionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRoomRepository _roomRepository;

    public QuestionCommandService(IQuestionRepository questionRepository,IUserRepository userRepository,IRoomRepository roomRepository)
    {
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _roomRepository = roomRepository;
    }

    public async Task<Guid> Handle(CreateQuestionCommand command)
    {
        
        var question = new Question(command.UserId, command.RoomId, command.Text);
        var requirement1 = await _userRepository.FindByIdAsync(command.UserId);
        var requirement2 =await _roomRepository.FindByIdAsync(command.RoomId);
        if (requirement2 != null && requirement1 != null)
        {
            await _questionRepository.AddAsync(question);

            return question.Id;
        }
        else
        {
            throw new InvalidOperationException("User or Room not found.");
        }
    }
    public async Task Handle(LikeQuestionCommand command)
    {
        var question = await _questionRepository.FindByIdAsync(command.QuestionId);
        if (question == null)
        {
            throw new InvalidOperationException("Question not found.");
        }
        question.Like();
        _questionRepository.UpdateAsync(question);
    }
}
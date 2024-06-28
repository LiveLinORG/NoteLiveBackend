using NoteLiveBackend.Room.Domain.Model.Commands;

namespace NoteLiveBackend.Room.Domain.Services;

public interface IQuestionCommandService
{
    Task<Guid> Handle(CreateQuestionCommand command);
    Task Handle(LikeQuestionCommand command);
    Task Handle(AnswerQuestionCommand command); 

}
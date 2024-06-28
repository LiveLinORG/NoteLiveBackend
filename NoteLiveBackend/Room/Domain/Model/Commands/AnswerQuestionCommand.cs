namespace NoteLiveBackend.Room.Domain.Model.Commands;

public record AnswerQuestionCommand(Guid id,string answer);
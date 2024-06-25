namespace NoteLiveBackend.Room.Domain.Model.Commands;

public record CreateQuestionCommand(Guid UserId, Guid RoomId, string Text);

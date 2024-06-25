namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

public record CreateQuestionResource(Guid UserId, Guid RoomId, string Text);

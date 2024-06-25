namespace NoteLiveBackend.Room.Domain.Model.Commands;

public record CheckIfActivatedCommand(Guid RoomId, Guid UserId);

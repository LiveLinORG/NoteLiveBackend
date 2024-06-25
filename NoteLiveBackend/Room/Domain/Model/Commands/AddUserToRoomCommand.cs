namespace NoteLiveBackend.Room.Domain.Model.Commands;


public record AddUserToRoomCommand(Guid RoomId, Guid UserId);

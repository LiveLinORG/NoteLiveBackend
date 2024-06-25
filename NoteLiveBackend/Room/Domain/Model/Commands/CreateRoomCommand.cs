namespace NoteLiveBackend.Room.Domain.Model.Commands;

public record CreateRoomCommand(string Name, Guid ProfessorId);

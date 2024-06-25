namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

public record CreateRoomResource
(string Name, Guid ProfessorId);
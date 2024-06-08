namespace NoteLiveBackend.Room.Domain.Model.Commands;

public class CreateRoomCommand
{
    public Guid RoomId { get; }
    public string RoomName { get; }
    public Guid ProfessorId { get; }

    public CreateRoomCommand(Guid roomId, string roomName, Guid professorId)
    {
        RoomId = roomId;
        RoomName = roomName;
        ProfessorId = professorId;
    }
}

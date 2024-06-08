namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public class RoomDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ProfessorId { get; set; }

    public RoomDto(Guid id, string name, Guid professorId)
    {
        Id = id;
        Name = name;
        ProfessorId = professorId;
    }
}

using NoteLiveBackend.Users.Domain.Model.Aggregates;

namespace NoteLiveBackend.Rooms.Domain.Model.Aggregates;

public partial class Session
{
    public int Id { get; }
    public int UserId { get; private set; }
    public int ChatboxId { get; private set; }
    public int PresentationId { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }
    public int MaxAttendees { get; private set; }
    public Alumno Alumno { get; internal set; }
    public Profesor Profesor { get; internal set; }
    public Chatbox Chatbox { get; internal set; }
    public Presentation Presentation { get; internal set; }
    
    
    public Session(int userId, int chatboxId, int presentationId, DateTime startTime, DateTime endTime,
        int maxAttendees) 
    {
        UserId = userId;
        ChatboxId = chatboxId;
        PresentationId = presentationId;
        StartTime = startTime;
        EndTime = endTime;
        MaxAttendees = maxAttendees;
    }
}
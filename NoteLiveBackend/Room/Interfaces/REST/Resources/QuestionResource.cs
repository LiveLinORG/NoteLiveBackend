namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

public class QuestionResource
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid RoomId { get; set; }
    public string Text { get; set; }
    public int Likes { get; set; }
    public string Answer { get; set; }
}
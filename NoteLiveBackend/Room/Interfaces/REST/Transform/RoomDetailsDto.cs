namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public class RoomDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ProfessorName { get; set; }
    public List<QuestionDto> Questions { get; set; }
    public List<ChatMessageDto> ChatMessages { get; set; }
    public PDFDto Pdf { get; set; }

    public RoomDetailsDto(Guid id, string name, string professorName, List<QuestionDto> questions, List<ChatMessageDto> chatMessages, PDFDto pdf)
    {
        Id = id;
        Name = name;
        ProfessorName = professorName;
        Questions = questions;
        ChatMessages = chatMessages;
        Pdf = pdf;
    }
}

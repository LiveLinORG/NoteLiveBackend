namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

public class PDFWithQuestionsResource
{
    public byte[] PDF { get; set; }
    public List<QuestionResource> Questions { get; set; }
}
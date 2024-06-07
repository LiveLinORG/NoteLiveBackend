namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class Room
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid ProfessorId { get; private set; }
    public PDF PDF { get; private set; }
    public List<Question> Questions { get; private set; }
    public List<Guid> UserIds { get; private set; }

    public Room(Guid id, string name, Guid professorId)
    {
        Id = id;
        Name = name;
        ProfessorId = professorId;
        Questions = new List<Question>();
        UserIds = new List<Guid>();
    }

    public void UploadPDF(PDF pdf)
    {
        PDF = pdf;
    }

    public void AskQuestion(Question question)
    {
        Questions.Add(question);
    }

    public void AddUser(Guid userId)
    {
        UserIds.Add(userId);
    }
}

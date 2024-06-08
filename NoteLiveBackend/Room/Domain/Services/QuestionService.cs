using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Domain.Services;

public class QuestionService
{
    public void AskQuestion(Model.Entities.Room room, Question question)
    {
        room.AskQuestion(question);
        // Lógica adicional para manejar la pregunta
    }
}

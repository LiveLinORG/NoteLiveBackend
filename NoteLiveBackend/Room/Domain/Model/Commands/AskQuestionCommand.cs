namespace NoteLiveBackend.Room.Domain.Model.Commands;

public class AskQuestionCommand
{
    public Guid RoomId { get; }
    public Guid QuestionId { get; }
    public Guid UserId { get; }
    public string Text { get; }

    public AskQuestionCommand(Guid roomId, Guid questionId, Guid userId, string text)
    {
        RoomId = roomId;
        QuestionId = questionId;
        UserId = userId;
        Text = text;
    }
}

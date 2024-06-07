namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class AskQuestionCommandService
{
    private readonly IRoomRepository _roomRepository;

    public AskQuestionCommandService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void Handle(AskQuestionCommand command)
    {
        var room = _roomRepository.GetById(command.RoomId);
        if (room == null) throw new RoomNotFoundException();

        var question = new Question(command.QuestionId, command.UserId, command.Text);
        room.AskQuestion(question);
        _roomRepository.Update(room);
    }
}

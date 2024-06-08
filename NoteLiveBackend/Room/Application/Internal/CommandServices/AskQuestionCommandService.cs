using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class AskQuestionCommandService
{
    private readonly IRoomRepository _roomRepository;

    public AskQuestionCommandService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public async Task HandleAsync(AskQuestionCommand command)
    {
        var room = await _roomRepository.GetById(command.RoomId);
        if (room == null) throw new RoomNotFoundException();

        var question = new Question(command.QuestionId, command.UserId, command.RoomId, command.Text);
        room.AskQuestion(question);
        await _roomRepository.Update(room);
    }

}

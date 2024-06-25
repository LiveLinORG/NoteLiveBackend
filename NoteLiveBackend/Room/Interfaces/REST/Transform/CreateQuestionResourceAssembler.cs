using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public static class CreateQuestionResourceAssembler
{
    public static CreateQuestionCommand ToCommand(CreateQuestionResource resource)
    {
        return new CreateQuestionCommand(resource.UserId, resource.RoomId, resource.Text);
    }
}
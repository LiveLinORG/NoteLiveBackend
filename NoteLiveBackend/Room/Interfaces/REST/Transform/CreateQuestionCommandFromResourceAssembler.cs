using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public class CreateQuestionCommandFromResourceAssembler
{
    public static CreateQuestionCommand ToCommandFromResource(CreateQuestionResource resource)
    {
        return new CreateQuestionCommand(resource.UserId, resource.RoomId, resource.Text);
    }
}
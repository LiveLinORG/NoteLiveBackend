using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public static class QuestionResourceAssembler
{
    public static QuestionResource ToResource(Question question)
    {
        return new QuestionResource
        {
            Id = question.Id,
            UserId = question.UserId,
            RoomId = question.RoomId,
            Text = question.Text,
            Likes = question.Likes,
            Answer = question.Answer
        };
    }
}

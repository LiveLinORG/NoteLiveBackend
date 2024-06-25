using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public static class PDFWithQuestionsResourceAssembler
{
    public static PDFWithQuestionsResource ToResourceFromEntity(
        (byte[]?, IReadOnlyList<Question?>) result)
    {
        return new PDFWithQuestionsResource
        {
            PDF = result.Item1,
            Questions = result.Item2?.Select(q => new QuestionResource
            {
                Id = q.Id,
                UserId = q.UserId,
                RoomId = q.RoomId,
                Text = q.Text,
                Likes = q.Likes
            }).ToList()
        };
    }

}
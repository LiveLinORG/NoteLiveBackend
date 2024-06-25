using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public static class PDFWithQuestionsResourceAssembler
{
    public static PDFWithQuestionsResource ToResourceFromEntity(Domain.Model.Entities.Room room)
    {
        return new PDFWithQuestionsResource
        {
            PDF = room.PDF.Content,
            Questions = room.Questions.Select(q => new QuestionResource
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
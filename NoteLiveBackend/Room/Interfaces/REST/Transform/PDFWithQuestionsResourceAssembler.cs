using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public static class PDFWithQuestionsResourceAssembler
{
    public static PDFWithQuestionsResource ToResourceFromEntity(
        (byte[]? pdfData, List<Domain.Model.Entities.Question>? questions) result)
    {
        return new PDFWithQuestionsResource
        {
            PDF = result.pdfData,
            Questions = result.questions?.Select(q => new QuestionResource
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
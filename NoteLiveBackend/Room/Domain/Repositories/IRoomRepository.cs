using NoteLiveBackend.Shared.Domain.Repositories;

namespace NoteLiveBackend.Room.Domain.Repositories;

public interface IRoomRepository : IBaseRepository<Model.Entities.Room>
{
   Task<IEnumerable<Model.Entities.Room>> FindByPdfNameAsync(string pdfName);
   new Task<Model.Entities.Room?> FindByIdAsync(Guid id);
   Task<Model.Entities.Room?> FindByIdWithChatAsync(Guid id);
   Task<Model.Entities.Room?> FindByIdWithChatAndPdfAsync(Guid id);

    Task<(byte[]?, List<Domain.Model.Entities.Question>?)> FindPdfAndQuestionsAsync(Guid id);
}
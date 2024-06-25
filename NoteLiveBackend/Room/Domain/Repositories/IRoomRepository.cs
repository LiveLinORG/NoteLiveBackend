using NoteLiveBackend.Shared.Domain.Repositories;

namespace NoteLiveBackend.Room.Domain.Repositories;

public interface IRoomRepository : IBaseRepository<Model.Entities.Room>
{
   Task<IEnumerable<Model.Entities.Room>> FindByPdfNameAsync(string pdfName);

}
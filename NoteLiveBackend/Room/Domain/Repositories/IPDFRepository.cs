using NoteLiveBackend.IAM.Domain.Model.Aggregates;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Shared.Domain.Repositories;

namespace NoteLiveBackend.Room.Domain.Repositories;

public interface IPDFRepository : IBaseRepository<PDF>
{
    PDF? FindPDFByIdSync(Guid pdfId);
    Task<PDF?> GetPDFByIdAsync(Guid pdfId);

}
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class PDFRepository : BaseRepository<PDF>, IPDFRepository
{
    public PDFRepository(AppDbContext context) : base(context) { }
    public PDF? FindPDFByIdSync(Guid pdfId)
    {
        return Context.Set<PDF>()
            .FirstOrDefault(p => p.Id == pdfId);
    }
}
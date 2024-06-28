using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Configuration;
using NoteLiveBackend.Shared.Infraestructure.Persistences.EFC.Repositories;

namespace NoteLiveBackend.Room.Infraestructure.Repositories;

public class PDFRepository : BaseRepository<PDF>, IPDFRepository
{
    /**
     * <summary>
     *  Initializes a new instance
     * </summary>
     * <param name="context">The DB context to be used by the repository.</param>
     */
    public PDFRepository(AppDbContext context) : base(context)
    {
    }
    
    /**
     * <summary>
     *  Finds a PDF by Id
     * </summary>
     * <param name="pdfId">The unique identifier of the PDF.</param>
     * <returns>The PDF entity if found; otherwise, null.</returns>
     */
    public PDF? FindPDFByIdSync(Guid pdfId)
    {
        return Context.Set<PDF>()
            .FirstOrDefault(p => p.Id == pdfId);
    }
    
    /**
     * <summary>
     *  Gets a PDF by id
     * </summary>
     * <param name="pdfId">The PDF id</param>
     * <returns>A task</returns>
     */
    public async Task<PDF?> GetPDFByIdAsync(Guid pdfId)
    {
        return await Context.PDFs.FindAsync(pdfId);
    }
}
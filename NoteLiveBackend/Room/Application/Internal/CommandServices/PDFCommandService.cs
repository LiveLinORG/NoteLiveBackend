using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Domain.Repositories;
using NoteLiveBackend.Room.Domain.Services;

namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class PDFCommandService : IPDFCommandService
{
    private readonly IPDFRepository _pdfRepository;

    public PDFCommandService(IPDFRepository pdfRepository)
    {
        _pdfRepository = pdfRepository ?? throw new ArgumentNullException(nameof(pdfRepository));
    }


    public PDF? associate(Guid pdfId)
    {
        PDF? pdf = _pdfRepository.FindPDFByIdSync(pdfId);
        return pdf;
    }
}
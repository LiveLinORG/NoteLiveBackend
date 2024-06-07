using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Application.Internal.Outboundservices.acl;

public interface IPDFExportService
{
    Task ExportAsync(PDF pdf, IEnumerable<Question> questions);
}

public class PDFExportService : IPDFExportService
{

    public Task ExportAsync(PDF pdf, IEnumerable<Question> questions)
    {
        //falta
        throw new NotImplementedException();
    }
}
namespace NoteLiveBackend.Room.Application.Internal.Outboundservices.acl;

public interface IPDFExportService
{
    void Export(PDF pdf, IEnumerable<Question> questions);
}

public class PDFExportService : IPDFExportService
{
    public void Export(PDF pdf, IEnumerable<Question> questions)
    {
        // Lógica para exportar el PDF incluyendo las preguntas
    }
}

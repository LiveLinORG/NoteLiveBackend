using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Domain.Services;

public class PDFService
{
    public void UploadPDF(Model.Entities.Room room, PDF? pdf)
    {
        room.UploadPDF(pdf);
        // Lógica adicional para manejar el PDF
    }

    public void ExportPDF(PDF pdf, IEnumerable<Question> questions)
    {
        // Lógica para exportar el PDF con las preguntas
    }
}

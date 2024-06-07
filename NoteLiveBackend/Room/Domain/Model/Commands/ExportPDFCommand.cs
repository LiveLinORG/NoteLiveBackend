namespace NoteLiveBackend.Room.Domain.Model.Commands;

public class ExportPDFCommand
{
    public Guid RoomId { get; }
    public Guid PdfId { get; }

    public ExportPDFCommand(Guid roomId, Guid pdfId)
    {
        RoomId = roomId;
        PdfId = pdfId;
    }
}

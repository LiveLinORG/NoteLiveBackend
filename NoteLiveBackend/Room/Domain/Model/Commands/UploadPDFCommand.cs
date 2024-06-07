namespace NoteLiveBackend.Room.Domain.Model.Commands;

public class UploadPDFCommand
{
    public Guid RoomId { get; }
    public Guid PdfId { get; }
    public byte[] PdfContent { get; }

    public UploadPDFCommand(Guid roomId, Guid pdfId, byte[] pdfContent)
    {
        RoomId = roomId;
        PdfId = pdfId;
        PdfContent = pdfContent;
    }
}

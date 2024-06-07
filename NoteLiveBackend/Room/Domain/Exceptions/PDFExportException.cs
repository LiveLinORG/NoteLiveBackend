namespace NoteLiveBackend.Room.Domain.Exceptions;

public class PDFExportException : Exception
{
    public PDFExportException() : base("PDF export failed") { }
}

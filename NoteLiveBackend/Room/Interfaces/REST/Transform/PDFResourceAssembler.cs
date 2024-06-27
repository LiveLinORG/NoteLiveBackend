using NoteLiveBackend.Room.Domain.Model.Entities;
using NoteLiveBackend.Room.Interfaces.REST.Resources;

namespace NoteLiveBackend.Room.Interfaces.REST.Transform;

public static class PDFResourceAssembler
{
    public static PDFResource ToResourceFromEntity(PDF pdf)
    {
        if (pdf == null)
            return null;

        return new PDFResource
        {
            Id = pdf.Id,
            Content = pdf.Content, 
        };
    }
}
using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Domain.Services;

public interface IPDFCommandService
{
    PDF? associate(Guid pdfId);
}
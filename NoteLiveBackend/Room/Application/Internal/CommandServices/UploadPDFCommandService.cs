using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Entities;

namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class UploadPDFCommandService
{
    private readonly IRoomRepository _roomRepository;

    public UploadPDFCommandService(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
    }

    public void Handle(UploadPDFCommand command)
    {
        var room = _roomRepository.GetById(command.RoomId);
        if (room == null) throw new RoomNotFoundException();

        var pdf = new PDF(command.PdfId, command.PdfContent);
        room.UploadPDF(pdf);
        _roomRepository.Update(room);
    }
}

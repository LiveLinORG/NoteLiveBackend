using NoteLiveBackend.Room.Application.Internal.Outboundservices.acl;
using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Room.Domain.Model.Commands;

namespace NoteLiveBackend.Room.Application.Internal.CommandServices;

public class ExportPDFCommandService
{
    private readonly IRoomRepository _roomRepository;
    private readonly IPDFExportService _pdfExportService;

    public ExportPDFCommandService(IRoomRepository roomRepository, IPDFExportService pdfExportService)
    {
        _roomRepository = roomRepository;
        _pdfExportService = pdfExportService;
    }

    public async Task HandleAsync(ExportPDFCommand command)
    {
        try
        {
            var room = await _roomRepository.GetById(command.RoomId);
            if (room == null) throw new RoomNotFoundException();

            var pdf = room.PDF;
            if (pdf == null) throw new PDFExportException();

            await room.ExportPDF(_pdfExportService); 
        }
        catch (Exception ex)
        {
            // Manejar la excepción aquí
        }
    }
}
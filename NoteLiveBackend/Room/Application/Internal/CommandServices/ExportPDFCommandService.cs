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

    public void Handle(ExportPDFCommand command)
    {
        var room = _roomRepository.GetById(command.RoomId);
        if (room == null) throw new RoomNotFoundException();

        var pdf = room.PDF;
        if (pdf == null) throw new PDFNotFoundException();

        _pdfExportService.Export(pdf, room.Questions);
    }
}

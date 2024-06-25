using NoteLiveBackend.Room.Application.Internal.Outboundservices.acl;
using NoteLiveBackend.Room.Domain.Exceptions;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Repositories;

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

 
}
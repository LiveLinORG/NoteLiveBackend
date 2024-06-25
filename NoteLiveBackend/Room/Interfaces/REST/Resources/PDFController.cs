using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

[ApiController]
[Route("api/[controller]")]
public class PDFController : ControllerBase
{
    private readonly GetPDFDetailsQueryService _getPDFDetailsQueryService;

    public PDFController(GetPDFDetailsQueryService getPDFDetailsQueryService)
    {
        _getPDFDetailsQueryService = getPDFDetailsQueryService;
    }



    [HttpGet("{roomId}")]
    public IActionResult GetPDFDetails(Guid roomId)
    {
        var query = new GetPDFDetailsQuery(roomId);
        var pdf = _getPDFDetailsQueryService.Handle(query);
        return Ok(pdf);
    }
}

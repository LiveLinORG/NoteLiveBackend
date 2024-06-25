using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Room.Interfaces.REST.Transform;

namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

[ApiController]
[Route("api/[controller]")]
public class PDFController : ControllerBase
{
    private readonly IPDFQueryService _pdfQueryService;

    public PDFController(IPDFQueryService pdfQueryService)
    {
        _pdfQueryService = pdfQueryService;
    }

    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetPDFWithQuestionsByRoomId([FromRoute] Guid roomId)
    {
        var query = new GetPDFWithQuestionsByRoomIdQuery(roomId);
        var result = await _pdfQueryService.Handle(query);
        if (result == (null, null)) return NotFound(new { message = "Room or PDF not found." });

        var resource = PDFWithQuestionsResourceAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }

}
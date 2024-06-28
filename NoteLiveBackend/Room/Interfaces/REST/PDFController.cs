using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Domain.Model.Queries;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Room.Interfaces.REST.Transform;

namespace NoteLiveBackend.Room.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class PDFController : ControllerBase
{
    private readonly IPDFQueryService _pdfQueryService;
    
    /**
     * <summary>
     *  Initializes a new instance of the "PDFController" class.
     * </summary>
     */
    public PDFController(IPDFQueryService pdfQueryService)
    {
        _pdfQueryService = pdfQueryService;
    }
    
    /**
     * <summary>
     *  Gets a PDF with associated questions by room ID.
     * </summary>
     * <param name="roomId">The unique identifier of the room.</param>
     * <returns>A task</returns>
     */
    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetPDFWithQuestionsByRoomId([FromRoute] Guid roomId)
    {
        var query = new GetPDFWithQuestionsByRoomIdQuery(roomId);
        var result = await _pdfQueryService.Handle(query);
        if (result == (null, null)) return NotFound(new { message = "Room or PDF not found." });

        var resource = PDFWithQuestionsResourceAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
    /**
     * <summary>
     *  Gets a PDF by its identifier.
     * </summary>
     * <param name="pdfId">The unique identifier of the PDF.</param>
     * <returns>A task</returns>
     */
    [HttpGet("getbyid/{pdfId:guid}")]
    public async Task<IActionResult> GetPDFById([FromRoute] Guid pdfId)
    {
        var query = new GetPDFByIdQuery(pdfId); 
        var result = await _pdfQueryService.Handle(query);
        if (result == null)
            return NotFound(new { message = "PDF not found." });

        var resource = PDFResourceAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
}
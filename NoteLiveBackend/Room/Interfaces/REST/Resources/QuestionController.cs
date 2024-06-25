using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

[ApiController]
[Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly GetQuestionsQueryService _getQuestionsQueryService;


    public QuestionController(GetQuestionsQueryService getQuestionsQueryService)
    {
        _getQuestionsQueryService = getQuestionsQueryService;
    }



    [HttpGet("{roomId}")]
    public IActionResult GetQuestions(Guid roomId)
    {
        var query = new GetQuestionsQuery(roomId);
        var questions = _getQuestionsQueryService.Handle(query);
        return Ok(questions);
    }
}

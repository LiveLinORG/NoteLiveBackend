using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Application.Internal.CommandServices;
using NoteLiveBackend.Room.Application.Internal.Queryservices;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Queries;

namespace NoteLiveBackend.Room.Interfaces.REST.Resources;

[ApiController]
[Microsoft.AspNetCore.Components.Route("api/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly AskQuestionCommandService _askQuestionCommandService;
    private readonly GetQuestionsQueryService _getQuestionsQueryService;

    public QuestionController(AskQuestionCommandService askQuestionCommandService, GetQuestionsQueryService getQuestionsQueryService)
    {
        _askQuestionCommandService = askQuestionCommandService;
        _getQuestionsQueryService = getQuestionsQueryService;
    }

    [HttpPost]
    public async Task<IActionResult> AskQuestion([FromBody] AskQuestionCommand command)
    {
        await _askQuestionCommandService.HandleAsync(command);
        return Ok();
    }


    [HttpGet("{roomId}")]
    public IActionResult GetQuestions(Guid roomId)
    {
        var query = new GetQuestionsQuery(roomId);
        var questions = _getQuestionsQueryService.Handle(query);
        return Ok(questions);
    }
}

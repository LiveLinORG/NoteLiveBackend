using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Domain.Services;
using NoteLiveBackend.Room.Interfaces.REST.Resources;
using NoteLiveBackend.Room.Interfaces.REST.Transform;

namespace NoteLiveBackend.Room.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class QuestionController : ControllerBase
{
    private readonly IQuestionQueryService _questionQueryService;
    private readonly IQuestionCommandService _questionCommandService;

    public QuestionController(IQuestionQueryService questionQueryService,IQuestionCommandService questionCommandService)
    {
        _questionQueryService = questionQueryService;
        _questionCommandService = questionCommandService;

    }

    
    //RECIBE public record CreateQuestionResource(Guid UserId, Guid RoomId, string Text);
    [HttpPost("postQuestion")]
    public async Task<IActionResult> PostQuestion([FromBody] CreateQuestionResource createQuestionResource)
    {
        var command = CreateQuestionResourceAssembler.ToCommand(createQuestionResource);
        var questionId = await _questionCommandService.Handle(command);

        return CreatedAtAction(nameof(PostQuestion), new { questionId }, null);
    }


}

using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Domain.Model.Commands;
using NoteLiveBackend.Room.Domain.Model.Queries;
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
    
    [HttpPatch("likeQuestion/{id}")]
    public async Task<IActionResult> LikeQuestion(Guid id)
    {
        var command = new LikeQuestionCommand(id);
        await _questionCommandService.Handle(command);
        return Ok();
    }
    [HttpPatch("answer/{id}")]
    public async Task<IActionResult> AnswerQuestion([FromRoute] Guid id, [FromBody] AnswerQuestionDto answerDto)
    {
        if (answerDto == null || string.IsNullOrEmpty(answerDto.Answer))
        {
            return BadRequest("Answer is required.");
        }

        var command = new AnswerQuestionCommand(id, answerDto.Answer);
        await _questionCommandService.Handle(command);
        return Ok();
    }


    [HttpGet("getQuestionsInRoom/{roomId}")]
    public async Task<IActionResult> GetQuestionsInRoom(Guid roomId)
    {
        var query = new GetQuestionsByRoomQuery(roomId);
        var questions = await _questionQueryService.Handle(query);

        if (questions == null || !questions.Any())
        {
            return NotFound(new { message = "No questions found in the specified room." });
        }

        var resources = questions.Select(QuestionResourceAssembler.ToResource);

        return Ok(resources);
    }
    
}

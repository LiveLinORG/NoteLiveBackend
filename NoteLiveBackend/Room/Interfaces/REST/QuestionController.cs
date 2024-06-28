using Microsoft.AspNetCore.Mvc;
using NoteLiveBackend.Room.Domain.Model.Commands;
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
    
    /**
     * <summary>
     *  Initializes a new instance of the "QuestionController" class.
     * </summary>
     * <param name="questionQueryService">The service for handling question queries.</param>
     * <param name="questionCommandService">The service for handling question commands.</param>
     */
    public QuestionController(IQuestionQueryService questionQueryService,IQuestionCommandService questionCommandService)
    {
        _questionQueryService = questionQueryService;
        _questionCommandService = questionCommandService;

    }

    
    /**
     * <summary>
     *  Posts a new question.
     * </summary>
     * <param name="createQuestionResource">The resource containing the details of the question to be created.</param>
     * <returns>A task</returns>
     */
    [HttpPost("postQuestion")]
    public async Task<IActionResult> PostQuestion([FromBody] CreateQuestionResource createQuestionResource)
    {
        var command = CreateQuestionResourceAssembler.ToCommand(createQuestionResource);
        var questionId = await _questionCommandService.Handle(command);

        return CreatedAtAction(nameof(PostQuestion), new { questionId }, null);
    }
    
    /**
     * <summary>
     *  Likes a question by its identifier.
     * </summary>
     * <param name="id">The id of the question.</param>
     * <returns>A task</returns>
     */
    [HttpPatch("likeQuestion/{id}")]
    public async Task<IActionResult> LikeQuestion(Guid id)
    {
        var command = new LikeQuestionCommand(id);
        await _questionCommandService.Handle(command);
        return Ok();
    }

}

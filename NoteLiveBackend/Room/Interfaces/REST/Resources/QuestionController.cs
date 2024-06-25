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
public class QuestionController : ControllerBase
{
    private readonly IQuestionQueryService _questionQueryService;
    private readonly IQuestionCommandService _questionCommandService;

    public QuestionController(IQuestionQueryService questionQueryService,IQuestionCommandService questionCommandService)
    {
        _questionQueryService = questionQueryService;
        _questionCommandService = questionCommandService;

    }


    [HttpPost("postQuestion")]
    public async Task<IActionResult> PostQuestion([FromBody] CreateQuestionResource createQuestionResource)
    {
        var command = CreateQuestionResourceAssembler.ToCommand(createQuestionResource);
        var questionId = await _questionCommandService.Handle(command);

        return CreatedAtAction(nameof(PostQuestion), new { questionId }, null);
    }


}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionCrudService questionCRUDService;
    private readonly IMapper mapper;

    public QuestionsController(IQuestionCrudService questionCRUDService, IMapper mapper)
    {
        this.questionCRUDService = questionCRUDService;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<List<QuestionDto>> GetAllAsync()
    {
        return mapper.Map<List<QuestionDto>>(await questionCRUDService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<QuestionDto> GetByIdAsync(Guid id)
    {
        return mapper.Map<QuestionDto>(await questionCRUDService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Question newQuestion)
    {
        await questionCRUDService.CreateAsync(newQuestion);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = newQuestion.Id }, mapper.Map<QuestionDto>(newQuestion));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, Question updatedQuestion)
    {
        await questionCRUDService.UpdateAsync(id, updatedQuestion);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await questionCRUDService.DeleteAsync(id);

        return NoContent();
    }
}

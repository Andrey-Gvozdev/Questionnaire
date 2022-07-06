using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class QuestionsDefinitionController : ControllerBase
{
    private readonly IQuestionDefinitionCrudService questionDefinitionCRUDService;
    private readonly IMapper mapper;

    public QuestionsDefinitionController(IQuestionDefinitionCrudService questionDefinitionCRUDService, IMapper mapper)
    {
        this.questionDefinitionCRUDService = questionDefinitionCRUDService;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<List<QuestionDefinitionDto>> GetAllAsync()
    {
        return mapper.Map<List<QuestionDefinitionDto>>(await questionDefinitionCRUDService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<QuestionDefinitionDto> GetByIdAsync(Guid id)
    {
        return mapper.Map<QuestionDefinitionDto>(await questionDefinitionCRUDService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(QuestionDefinition newQuestionDefinition)
    {
        await questionDefinitionCRUDService.CreateAsync(newQuestionDefinition);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = newQuestionDefinition.Id }, mapper.Map<QuestionDefinitionDto>(newQuestionDefinition));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, QuestionDefinition updatedQuestionDefinition)
    {
        await questionDefinitionCRUDService.UpdateAsync(id, updatedQuestionDefinition);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await questionDefinitionCRUDService.DeleteAsync(id);

        return NoContent();
    }
}

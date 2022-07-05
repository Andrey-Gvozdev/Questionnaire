using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class QuestionsDefinitionController : ControllerBase
{
    private readonly IQuestionDefinitionCrudService questionDefinitionCRUDService;

    public QuestionsDefinitionController(IQuestionDefinitionCrudService questionDefinitionCRUDService)
    {
        this.questionDefinitionCRUDService = questionDefinitionCRUDService;
    }

    [HttpGet]
    public Task<List<QuestionDefinition>> GetAllAsync()
    {
        return questionDefinitionCRUDService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public Task<QuestionDefinition> GetByIdAsync(Guid id)
    {
        return questionDefinitionCRUDService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(QuestionDefinition newQuestionDefinition)
    {
        await questionDefinitionCRUDService.CreateAsync(newQuestionDefinition);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = newQuestionDefinition.Id }, newQuestionDefinition);
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

        return Ok();
    }
}

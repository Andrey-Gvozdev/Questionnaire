using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionCrudService questionCRUDService;

    public QuestionsController(IQuestionCrudService questionCRUDService)
    {
        this.questionCRUDService = questionCRUDService;
    }

    [HttpGet]
    public Task<List<Question>> GetAllAsync()
    {
        return questionCRUDService.GetAllAsync();
    }

    [HttpGet("{id}")]
    public Task<Question> GetByIdAsync(Guid id)
    {
        return questionCRUDService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Question newQuestion)
    {
        await questionCRUDService.CreateAsync(newQuestion);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = newQuestion.Id }, newQuestion);
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

        return Ok();
    }
}

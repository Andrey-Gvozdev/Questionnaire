using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class SurveysController : ControllerBase
{
    private readonly ISurveyCrudService surveyCrudService;

    public SurveysController(ISurveyCrudService surveyCrudService)
    {
        this.surveyCrudService = surveyCrudService;
    }

    [HttpGet]
    public async Task<List<Survey>> GetAllAsync() =>
        await surveyCrudService.GetAllAsync();

    [HttpGet("{id}")]
    public Task<Survey> GetByIdAsync(Guid id)
    {
        return surveyCrudService.GetByIdAsync(id);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Survey newSurvey)
    {
        await surveyCrudService.CreateAsync(newSurvey);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = newSurvey.Id }, newSurvey);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, Survey updatedSurvey)
    {
        await surveyCrudService.UpdateAsync(id, updatedSurvey);

        return Ok("Item updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await surveyCrudService.DeleteAsync(id);

        return Ok("Item deleted");
    }
}

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

    // TODO: if most of you code use method bodies, better to leave the body
    // even for single-line methods, just to have unified code style
    [HttpGet]
    public async Task<List<Survey>> GetAllAsync() =>
        await surveyCrudService.GetAllAsync();

    [HttpGet("{id}")]
    [ActionName("GEtByIdAsync")]
    public Task<Survey> GetByIdAsync(Guid id)
    {
        return surveyCrudService.GetByIdAsync(id);
    }

    // TODO: maybe, better to have additional CreateSurveyRequestDto model
    // with no ID field and generate this field on BE instead of checking if
    // the survey with such ID already exists or not
    // TODO: also, please, do not use service level models for communication with
    // API clients, DTO model must be suitable for communication between client and API,
    // service model must be suitable for internal processing, and they should not depend
    // on each other
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

        // TODO: just 200 OK will be enough, no text inside needed
        // 200 OK after PUT operation exactly say that the item was updated
        // optionally, you can return updated model back to client, if it will
        // be helpful for client app
        return Ok("Item updated");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await surveyCrudService.DeleteAsync(id);

        // TODO: the same, just 200 OK will be enough,
        // or 204 No Content if you want to follow REST in
        // every minor detail
        return Ok("Item deleted");
    }
}

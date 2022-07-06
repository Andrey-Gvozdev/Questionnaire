using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers;

[ApiController]
[Route("/api/[controller]/")]
public class SurveysController : ControllerBase
{
    private readonly ISurveyCrudService surveyCrudService;
    private readonly IMapper mapper;

    public SurveysController(ISurveyCrudService surveyCrudService, IMapper mapper)
    {
        this.surveyCrudService = surveyCrudService;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<List<SurveyDto>> GetAllAsync()
    {
        return mapper.Map<List<SurveyDto>>(await surveyCrudService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<SurveyDto> GetByIdAsync(Guid id)
    {
        return mapper.Map<SurveyDto>(await surveyCrudService.GetByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Survey newSurvey)
    {
        await surveyCrudService.CreateAsync(newSurvey);

        return CreatedAtAction(nameof(GetByIdAsync), new { id = newSurvey.Id }, mapper.Map<SurveyDto>(newSurvey));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(Guid id, Survey updatedSurvey)
    {
        await surveyCrudService.UpdateAsync(id, updatedSurvey);

        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        await surveyCrudService.DeleteAsync(id);

        return NoContent();
    }
}

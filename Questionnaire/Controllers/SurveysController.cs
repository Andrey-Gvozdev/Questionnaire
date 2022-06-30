using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers
{
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
        public async Task<List<Survey>> Get() =>
            await surveyCrudService.Get();

        [HttpGet("{id}")]
        public Task<Survey> Get(Guid id)
        {
            return surveyCrudService.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Survey newSurvey)
        {
            await surveyCrudService.Create(newSurvey);

            return CreatedAtAction(nameof(Get), new { id = newSurvey.Id }, newSurvey);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Survey updatedSurvey)
        {
            await surveyCrudService.Update(id, updatedSurvey);

            return Ok("Item updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await surveyCrudService.Delete(id);

            return Ok("Item deleted");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionCRUDService questionCRUDService;

        public QuestionsController(IQuestionCRUDService questionCRUDService)
        {
            this.questionCRUDService = questionCRUDService;
        }

        [HttpGet]
        public async Task<List<Question>> Get() =>
            await questionCRUDService.Get();

        [HttpGet("{id}")]
        public Task<Question> Get(Guid id)
        {
            return questionCRUDService.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Question newQuestion)
        {
            await questionCRUDService.Create(newQuestion);

            return CreatedAtAction(nameof(Get), new { id = newQuestion.Id }, newQuestion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Question updatedQuestion)
        {
            await questionCRUDService.Update(id, updatedQuestion);

            return Ok("Item updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await questionCRUDService.Delete(id);

            return Ok("Item deleted");
        }
    }
}

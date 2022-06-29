using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers
{
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
        public async Task<List<QuestionDefinition>> Get() =>
            await questionDefinitionCRUDService.Get();

        [HttpGet("{id}")]
        public Task<QuestionDefinition> Get(Guid id)
        {
            return questionDefinitionCRUDService.Get(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(QuestionDefinition newQuestionDefinition)
        {
            await questionDefinitionCRUDService.Create(newQuestionDefinition);

            return CreatedAtAction(nameof(Get), new { id = newQuestionDefinition.Id }, newQuestionDefinition);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, QuestionDefinition updatedQuestionDefinition)
        {
            await questionDefinitionCRUDService.Update(id, updatedQuestionDefinition);

            return Ok("Item updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await questionDefinitionCRUDService.Delete(id);

            return Ok("Item deleted");
        }
    }
}

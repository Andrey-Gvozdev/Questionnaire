using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class QuestionsDefinitionController : ControllerBase
    {
        private readonly IQuestionDefinitionCRUDService questionDefinitionCRUDService;

        public QuestionsDefinitionController(IQuestionDefinitionCRUDService questionDefinitionCRUDService)
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
        public Task Post(QuestionDefinition newQuestionDefinition)
        {
            return questionDefinitionCRUDService.Create(newQuestionDefinition);
        }
    }
}

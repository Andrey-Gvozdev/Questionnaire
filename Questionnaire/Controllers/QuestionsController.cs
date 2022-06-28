using Microsoft.AspNetCore.Mvc;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;

namespace Questionnaire.Controllers
{
    [ApiController]
    [Route("/[controller]/")]
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
        public Task Post(Question newQuestion)
        {
            return questionCRUDService.Create(newQuestion);
        }
    }
}

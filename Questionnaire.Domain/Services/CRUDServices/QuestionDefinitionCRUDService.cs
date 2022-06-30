using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices
{
    public class QuestionDefinitionCrudService : IQuestionDefinitionCrudService
    {
        private readonly IQuestionDefinitionRepository questionDefinitionRepository;

        public QuestionDefinitionCrudService(IQuestionDefinitionRepository repository)
        {
            questionDefinitionRepository = repository;
        }

        public async Task<List<QuestionDefinition>> Get() =>
            await questionDefinitionRepository.Get();

        public async Task<QuestionDefinition> Get(Guid id)
        {
            var question = await questionDefinitionRepository.Get(id);

            return question ?? throw new NotFoundException("Item not found");
        }

        public async Task Create(QuestionDefinition newQuestionDefinition)
        {
            await questionDefinitionRepository.Create(newQuestionDefinition);
        }

        public async Task Update(Guid id, QuestionDefinition updatedQuestionDefinition)
        {
            await Get(id);
            await questionDefinitionRepository.Update(id, updatedQuestionDefinition);
        }

        public async Task Delete(Guid id)
        {
            await Get(id);
            await questionDefinitionRepository.Delete(id);
        }
    }
}

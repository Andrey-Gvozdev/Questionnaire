using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices
{
    public class QuestionDefinitionCRUDService : IQuestionDefinitionCRUDService
    {
        private readonly IQuestionDefinitionRepository questionDefinitionRepository;

        public QuestionDefinitionCRUDService(IQuestionDefinitionRepository repository)
        {
            questionDefinitionRepository = repository;
        }

        public async Task<List<QuestionDefinition>> Get() =>
            await questionDefinitionRepository.Get();

        public async Task<QuestionDefinition> Get(Guid id) =>
            await questionDefinitionRepository.Get(id);

        public async Task Create(QuestionDefinition newQuestionDefinition)
        {
            await questionDefinitionRepository.Create(newQuestionDefinition);
        }

        public async Task Update(Guid id, QuestionDefinition updatedQuestionDefinition)
        {
            await questionDefinitionRepository.Update(id, updatedQuestionDefinition);
        }

        public async Task Delete(Guid id)
        {
            await questionDefinitionRepository.Delete(id);
        }
    }
}

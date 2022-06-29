using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices
{
    public interface IQuestionDefinitionCrudService
    {
        Task<List<QuestionDefinition>> Get();

        Task<QuestionDefinition> Get(Guid id);

        Task Create(QuestionDefinition newQuestionDefinition);

        Task Update(Guid id, QuestionDefinition updatedQuestionDefinition);

        Task Delete(Guid id);
    }
}

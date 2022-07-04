using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices;

public interface IQuestionDefinitionCrudService
{
    Task<List<QuestionDefinition>> GetAllAsync();

    Task<QuestionDefinition> GetByIdAsync(Guid id);

    Task CreateAsync(QuestionDefinition newQuestionDefinition);

    Task UpdateAsync(Guid id, QuestionDefinition updatedQuestionDefinition);

    Task DeleteAsync(Guid id);
}

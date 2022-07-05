using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Data;

// TODO: Model folder is definetely a wrong place for this interface,
// folder Data inside Domain project would work
public interface IQuestionDefinitionRepository
{
    Task<List<QuestionDefinition>> GetAllAsync();

    Task<QuestionDefinition> GetByIdAsync(Guid id);

    Task CreateAsync(QuestionDefinition newQuestionDefinition);

    Task UpdateAsync(Guid id, QuestionDefinition updatedQuestionDefinition);

    Task DeleteAsync(Guid id);
}

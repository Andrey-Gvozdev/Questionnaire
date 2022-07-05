using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Data;

public interface IQuestionRepository
{
    Task<List<Question>> GetAllAsync();

    Task<Question> GetByIdAsync(Guid id);

    Task CreateAsync(Question newQuestion);

    Task UpdateAsync(Guid id, Question updatedQuestion);

    Task DeleteAsync(Guid id);
}

using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices;

public interface IQuestionCrudService
{
    Task<List<Question>> GetAllAsync();

    Task<Question> GetByIdAsync(Guid id);

    Task CreateAsync(Question newQuestion);

    Task UpdateAsync(Guid id, Question updatedQuestion);

    Task DeleteAsync(Guid id);
}

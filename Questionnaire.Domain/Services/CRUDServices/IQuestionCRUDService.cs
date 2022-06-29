using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices
{
    public interface IQuestionCrudService
    {
        Task<List<Question>> Get();

        Task<Question> Get(Guid id);

        Task Create(Question newQuestion);

        Task Update(Guid id, Question updatedQuestion);

        Task Delete(Guid id);
    }
}

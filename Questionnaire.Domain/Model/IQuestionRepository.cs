namespace Questionnaire.Domain.Model
{
    public interface IQuestionRepository
    {
        Task<List<Question>> Get();

        Task<Question> Get(Guid id);

        Task Create(Question newQuestion);

        Task Update(Guid id, Question updatedQuestion);

        Task Delete(Guid id);
    }
}

namespace Questionnaire.Domain.Model
{
    public interface IQuestionDefinitionRepository
    {
        Task<List<QuestionDefinition>> Get();

        Task<QuestionDefinition> Get(Guid id);

        Task Create(QuestionDefinition newQuestionDefinition);

        Task Update(Guid id, QuestionDefinition updatedQuestionDefinition);

        Task Delete(Guid id);
    }
}

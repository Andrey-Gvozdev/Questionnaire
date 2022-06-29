namespace Questionnaire.Domain.Model
{
    public interface ISurveyRepository
    {
        Task<List<Survey>> Get();

        Task<Survey> Get(Guid id);

        Task Create(Survey newSurvey);

        Task Update(Guid id, Survey updatedSurvey);

        Task Delete(Guid id);
    }
}

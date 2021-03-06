namespace Questionnaire.Domain.Model;

public interface ISurveyRepository
{
    Task<List<Survey>> GetAllAsync();

    Task<Survey> GetByIdAsync(Guid id);

    Task CreateAsync(Survey newSurvey);

    Task UpdateAsync(Guid id, Survey updatedSurvey);

    Task DeleteAsync(Guid id);
}

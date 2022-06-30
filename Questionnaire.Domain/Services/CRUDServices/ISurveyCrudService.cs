using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices;

public interface ISurveyCrudService
{
    Task<List<Survey>> GetAllAsync();

    Task<Survey> GetByIdAsync(Guid id);

    Task CreateAsync(Survey newSurvey);

    Task UpdateAsync(Guid id, Survey updatedSurvey);

    Task DeleteAsync(Guid id);
}

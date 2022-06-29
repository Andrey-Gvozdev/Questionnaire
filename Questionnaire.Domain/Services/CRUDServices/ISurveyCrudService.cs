using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices
{
    public interface ISurveyCrudService
    {
        Task<List<Survey>> Get();

        Task<Survey> Get(Guid id);

        Task Create(Survey newSurvey);

        Task Update(Guid id, Survey updatedSurvey);

        Task Delete(Guid id);
    }
}

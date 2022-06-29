using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices
{
    public class SurveyCrudService : ISurveyCrudService
    {
        private readonly ISurveyRepository surveyRepository;

        public SurveyCrudService(ISurveyRepository repository)
        {
            surveyRepository = repository;
        }

        public async Task<List<Survey>> Get() =>
            await surveyRepository.Get();

        public async Task<Survey> Get(Guid id)
        {
            var survey = await surveyRepository.Get(id);

            return survey ?? throw new NotFoundException("Item not found");
        }

        public async Task Create(Survey newSurvey)
        {
            await surveyRepository.Create(newSurvey);
        }

        public async Task Update(Guid id, Survey updatedSurvey)
        {
            await Get(id);
            await surveyRepository.Update(id, updatedSurvey);
        }

        public async Task Delete(Guid id)
        {
            await Get(id);
            await surveyRepository.Delete(id);
        }
    }
}

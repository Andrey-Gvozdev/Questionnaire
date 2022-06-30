using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Domain.Services.CRUDServices;

public class SurveyCrudService : ISurveyCrudService
{
    private readonly ISurveyRepository surveyRepository;

    public SurveyCrudService(ISurveyRepository repository)
    {
        surveyRepository = repository;
    }

    public async Task<List<Survey>> GetAllAsync() =>
        await surveyRepository.GetAllAsync();

    public async Task<Survey> GetByIdAsync(Guid id)
    {
        var survey = await surveyRepository.GetByIdAsync(id);

        return survey ?? throw new NotFoundException("Item not found");
    }

    public async Task CreateAsync(Survey newSurvey)
    {
        if (await surveyRepository.GetByIdAsync(newSurvey.Id) != null)
            throw new ValidationException(String.Concat("Item vith id: ", newSurvey.Id ," already exists"));
        else
            await surveyRepository.CreateAsync(newSurvey);
    }

    public async Task UpdateAsync(Guid id, Survey updatedSurvey)
    {
        await GetByIdAsync(id);
        await surveyRepository.UpdateAsync(id, updatedSurvey);
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        await surveyRepository.DeleteAsync(id);
    }
}

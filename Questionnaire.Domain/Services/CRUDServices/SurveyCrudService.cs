using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Data;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.ValidationServices;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Domain.Services.CRUDServices;

public class SurveyCrudService : ISurveyCrudService
{
    private readonly ISurveyRepository surveyRepository;
    private readonly ISurveyValidationService surveyValidationService;

    public SurveyCrudService(ISurveyRepository repository, ISurveyValidationService surveyValidationService)
    {
        surveyRepository = repository;
        this.surveyValidationService = surveyValidationService;
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
        surveyValidationService.ValidationSurvey(newSurvey);

        await surveyRepository.CreateAsync(newSurvey);
    }

    public async Task UpdateAsync(Guid id, Survey updatedSurvey)
    {
        await GetByIdAsync(id);
        surveyValidationService.ValidationSurvey(updatedSurvey);
        await surveyRepository.UpdateAsync(id, updatedSurvey);
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        await surveyRepository.DeleteAsync(id);
    }
}

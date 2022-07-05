using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.ValidationServices;

public interface ISurveyValidationService
{
    public void ValidationSurvey(Survey survey);
}
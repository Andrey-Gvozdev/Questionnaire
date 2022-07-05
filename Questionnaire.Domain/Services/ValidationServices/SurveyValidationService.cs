using Questionnaire.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Domain.Services.ValidationServices;

public class SurveyValidationService : ISurveyValidationService
{
    public void ValidationSurvey(Survey survey)
    {
        if (string.IsNullOrWhiteSpace(survey.Name))
        {
            throw new ValidationException("Survey name can't be empty");
        }
        if (survey.Name.Length > 50)
        {
            throw new ValidationException("Survey name can't be longer 50 charaсters");
        }
        if (survey.Questions.Count < 1)
        {
            throw new ValidationException("Survey must contain questions");
        }
    }
}

using Questionnaire.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Domain.Services.ValidationServices;

public class QuestionDefinitionValidationService : IQuestionDefinitionValidationService
{
    public void ValidationQuestion(QuestionDefinition questionDefinition)
    {
        if (string.IsNullOrWhiteSpace(questionDefinition.Name))
        {
            throw new ValidationException("QuestionDefinition name can't be empty");
        }
        if (questionDefinition.Name.Length > 50)
        {
            throw new ValidationException("QuestionDefinition name can't be longer 50 charaсters");
        }
        if (questionDefinition.Validation.MinValue > questionDefinition.Validation.MaxValue ||
            questionDefinition.Validation.MinLength > questionDefinition.Validation.MaxLength)
        {
            throw new ValidationException("Min value can't be less than max");
        }
    }
}

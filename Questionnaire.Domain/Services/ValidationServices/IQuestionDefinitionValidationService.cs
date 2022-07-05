using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.ValidationServices;

public interface IQuestionDefinitionValidationService
{
    public void ValidationQuestion(QuestionDefinition questionDefinition);
}
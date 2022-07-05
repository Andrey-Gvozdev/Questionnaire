using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.ValidationServices;

public interface IQuestionValidationService
{
    public Task ValidationQuestion(Question question);
}
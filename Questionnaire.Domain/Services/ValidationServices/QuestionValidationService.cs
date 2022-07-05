using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Domain.Services.ValidationServices
{
    public class QuestionValidationService : IQuestionValidationService
    {
        private IQuestionDefinitionCrudService questionDefinitionCrudService;

        public QuestionValidationService(IQuestionDefinitionCrudService questionDefinitionCrudService)
        {
            this.questionDefinitionCrudService = questionDefinitionCrudService;
        }
        public async Task ValidationQuestion(Question question)
        {
            if (string.IsNullOrWhiteSpace(question.QuestionText))
            {
                throw new ValidationException("Question field can't be empty");
            }
            try
            {
                await questionDefinitionCrudService.GetByIdAsync(question.Definition.Id);
            }
            catch
            {
                throw new ValidationException("Selected Question Definition not found"); 
            }
        }
    }
}

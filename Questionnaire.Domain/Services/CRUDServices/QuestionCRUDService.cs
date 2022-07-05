using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Data;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.ValidationServices;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Domain.Services.CRUDServices;

public class QuestionCrudService : IQuestionCrudService
{
    private readonly IQuestionRepository questionRepository;
    IQuestionValidationService questionValidationService;

    public QuestionCrudService(IQuestionRepository repository, IQuestionValidationService questionValidationService)
    {
        questionRepository = repository;
        this.questionValidationService = questionValidationService;
    }
    
    public async Task<List<Question>> GetAllAsync() =>
        await questionRepository.GetAllAsync();

    public async Task<Question> GetByIdAsync(Guid id)
    {
        var question = await questionRepository.GetByIdAsync(id);

        return question ?? throw new NotFoundException("Item not found");
    }

    public async Task CreateAsync(Question newQuestion)
    {
        if (await questionRepository.GetByIdAsync(newQuestion.Id) != null)
            throw new ValidationException(String.Concat("Item vith id: ", newQuestion.Id, " already exists"));
        await questionValidationService.ValidationQuestion(newQuestion);
        
        await questionRepository.CreateAsync(newQuestion);
    }

    public async Task UpdateAsync(Guid id, Question updatedQuestion)
    {
        await GetByIdAsync(id);
        await questionValidationService.ValidationQuestion(updatedQuestion);
        await questionRepository.UpdateAsync(id, updatedQuestion);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        await questionRepository.DeleteAsync(id);
    }
}

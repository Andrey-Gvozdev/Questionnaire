using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Data;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.ValidationServices;

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

    public Task<List<Question>> GetAllAsync() 
    {
        return questionRepository.GetAllAsync(); 
    }

    public Task<Question> GetByIdAsync(Guid id)
    {
        return questionRepository.GetByIdAsync(id) ?? throw new NotFoundException("Item not found");
    }

    public async Task CreateAsync(Question newQuestion)
    {
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

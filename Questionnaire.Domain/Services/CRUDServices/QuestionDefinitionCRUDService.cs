using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Data;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.ValidationServices;

namespace Questionnaire.Domain.Services.CRUDServices;

public class QuestionDefinitionCrudService : IQuestionDefinitionCrudService
{
    private readonly IQuestionDefinitionRepository questionDefinitionRepository;
    private readonly IQuestionDefinitionValidationService questionDefinitionValidationService;

    public QuestionDefinitionCrudService(IQuestionDefinitionRepository repository, IQuestionDefinitionValidationService questionDefinitionValidationService)
    {
        questionDefinitionRepository = repository;
        this.questionDefinitionValidationService = questionDefinitionValidationService;
    }

    public Task<List<QuestionDefinition>> GetAllAsync() 
    { 
        return questionDefinitionRepository.GetAllAsync(); 
    }

    public Task<QuestionDefinition> GetByIdAsync(Guid id)
    {
        return questionDefinitionRepository.GetByIdAsync(id) ?? throw new NotFoundException("Item not found");
    }

    public async Task CreateAsync(QuestionDefinition newQuestionDefinition)
    {
        questionDefinitionValidationService.ValidationQuestion(newQuestionDefinition);
        await questionDefinitionRepository.CreateAsync(newQuestionDefinition);
    }

    public async Task UpdateAsync(Guid id, QuestionDefinition updatedQuestionDefinition)
    {
        await GetByIdAsync(id);
        questionDefinitionValidationService.ValidationQuestion(updatedQuestionDefinition);
        await questionDefinitionRepository.UpdateAsync(id, updatedQuestionDefinition);
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        await questionDefinitionRepository.DeleteAsync(id);
    }
}

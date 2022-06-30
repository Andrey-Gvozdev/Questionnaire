using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices;

public class QuestionDefinitionCrudService : IQuestionDefinitionCrudService
{
    private readonly IQuestionDefinitionRepository questionDefinitionRepository;

    public QuestionDefinitionCrudService(IQuestionDefinitionRepository repository)
    {
        questionDefinitionRepository = repository;
    }

    public async Task<List<QuestionDefinition>> GetAllAsync() =>
        await questionDefinitionRepository.GetAllAsync();

    public async Task<QuestionDefinition> GetByIdAsync(Guid id)
    {
        var question = await questionDefinitionRepository.GetByIdAsync(id);

        return question ?? throw new NotFoundException("Item not found");
    }

    public async Task CreateAsync(QuestionDefinition newQuestionDefinition)
    {
        await questionDefinitionRepository.CreateAsync(newQuestionDefinition);
    }

    public async Task UpdateAsync(Guid id, QuestionDefinition updatedQuestionDefinition)
    {
        await GetByIdAsync(id);
        await questionDefinitionRepository.UpdateAsync(id, updatedQuestionDefinition);
    }

    public async Task DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        await questionDefinitionRepository.DeleteAsync(id);
    }
}

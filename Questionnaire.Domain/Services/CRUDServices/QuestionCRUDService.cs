using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace Questionnaire.Domain.Services.CRUDServices;

public class QuestionCrudService : IQuestionCrudService
{
    private readonly IQuestionRepository questionRepository;

    public QuestionCrudService(IQuestionRepository repository)
    {
        questionRepository = repository;
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
        else
            await questionRepository.CreateAsync(newQuestion);
    }

    public async Task UpdateAsync(Guid id, Question updatedQuestion)
    {
        await GetByIdAsync(id);
        await questionRepository.UpdateAsync(id, updatedQuestion);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        await GetByIdAsync(id);
        await questionRepository.DeleteAsync(id);
    }
}

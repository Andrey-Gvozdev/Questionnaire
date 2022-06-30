using Questionnaire.Domain.Model;

// TODO: you can skip a "namespace" pair of curvy brackets in .NET 6
namespace Questionnaire.Domain.Services.CRUDServices;

public interface IQuestionCrudService
{
    // TODO: better to use GetAll()/GetList() and GetById()
    // TODO: also there is a practice to use Async postfix in async methods,
    // but whether to use it or not - up to you
    Task<List<Question>> Get();

    Task<Question> Get(Guid id);

    Task Create(Question newQuestion);

    Task Update(Guid id, Question updatedQuestion);

    Task Delete(Guid id);
}

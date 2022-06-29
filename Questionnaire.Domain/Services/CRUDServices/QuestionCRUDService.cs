using Questionnaire.Domain.CustomExceptions;
using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices
{
    public class QuestionCrudService : IQuestionCrudService
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionCrudService(IQuestionRepository repository)
        {
            questionRepository = repository;
        }
        
        public async Task<List<Question>> Get() =>
            await questionRepository.Get();

        public async Task<Question> Get(Guid id)
        {
            var question = await questionRepository.Get(id);

            return question ?? throw new NotFoundException("Item not found");
        }

        public async Task Create(Question newQuestion)
        {
            await questionRepository.Create(newQuestion);
        }

        public async Task Update(Guid id, Question updatedQuestion)
        {
            await Get(id);
            await questionRepository.Update(id, updatedQuestion);
        }
        
        public async Task Delete(Guid id)
        {
            await Get(id);
            await questionRepository.Delete(id);
        }
    }
}

using Questionnaire.Domain.Model;

namespace Questionnaire.Domain.Services.CRUDServices
{
    public class QuestionCRUDService : IQuestionCRUDService
    {
        private readonly IQuestionRepository questionRepository;

        public QuestionCRUDService(IQuestionRepository repository)
        {
            questionRepository = repository;
        }
        
        public async Task<List<Question>> Get() =>
            await questionRepository.Get();

        public async Task<Question> Get(Guid id) =>
            await questionRepository.Get(id);

        public async Task Create(Question newQuestion)
        {
            await questionRepository.Create(newQuestion);
        }

        public async Task Update(Guid id, Question updatedQuestion)
        {
            await questionRepository.Update(id, updatedQuestion);
        }
        
        public async Task Delete(Guid id)
        {
            await questionRepository.Delete(id);
        }
    }
}

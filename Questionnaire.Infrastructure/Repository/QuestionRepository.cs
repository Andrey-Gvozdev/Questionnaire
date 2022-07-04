using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Questionnaire.Domain.Model;
using MongoDB.Bson;

namespace Questionnaire.Infrastructure.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoCollection<Question> questionsCollection;

        public QuestionRepository(IOptions<QuestionnaireDBSettings> questionnaireDBSettings)
        {
            var mongoClient = new MongoClient(questionnaireDBSettings.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(questionnaireDBSettings.Value.DatabaseName);

            questionsCollection = mongoDB.GetCollection<Question>(questionnaireDBSettings.Value.QuestionsCollectionName);
        }

        public async Task<List<Question>> Get() => 
            await questionsCollection.Find(_ => true).ToListAsync();

        public async Task<Question> Get(Guid id) =>
            await questionsCollection.Find(q => q.Id == id).FirstOrDefaultAsync();

        public async Task Create(Question newQuestion) =>
            await questionsCollection.InsertOneAsync(newQuestion);

        public async Task Update(Guid id, Question updatedQuestion) =>
            await questionsCollection.UpdateOneAsync(
                Builders<Question>.Filter.Eq(q => q.Id, id),
                Builders<Question>.Update
                    .Set(q => q.Definition, updatedQuestion.Definition)
                    .Set(q => q.QuestionText, updatedQuestion.QuestionText)
                    .Set(q => q.IsRequired, updatedQuestion.IsRequired)
                );

        public async Task Delete(Guid id) =>
            await questionsCollection.DeleteOneAsync(q => q.Id == id);
    }
}

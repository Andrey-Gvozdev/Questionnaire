using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Data;

namespace Questionnaire.Infrastructure.Repository;

public class QuestionRepository : IQuestionRepository
{
    private readonly IMongoCollection<Question> questionsCollection;

    public QuestionRepository(IOptions<QuestionnaireDBSettings> questionnaireDBSettings)
    {
        var mongoClient = new MongoClient(questionnaireDBSettings.Value.ConnectionString);
        var mongoDB = mongoClient.GetDatabase(questionnaireDBSettings.Value.DatabaseName);

        questionsCollection = mongoDB.GetCollection<Question>(questionnaireDBSettings.Value.QuestionsCollectionName);
    }

    public async Task<List<Question>> GetAllAsync() => 
        await questionsCollection.Find(_ => true).ToListAsync();

    public async Task<Question> GetByIdAsync(Guid id) =>
        await questionsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Question newQuestion) =>
        await questionsCollection.InsertOneAsync(newQuestion);

    public async Task UpdateAsync(Guid id, Question updatedQuestion) =>
        await questionsCollection.UpdateOneAsync(
            Builders<Question>.Filter.Eq(q => q.Id, id),
            Builders<Question>.Update
                .Set(q => q.Definition, updatedQuestion.Definition)
                .Set(q => q.QuestionText, updatedQuestion.QuestionText)
                .Set(q => q.IsRequired, updatedQuestion.IsRequired)
            );

    public async Task DeleteAsync(Guid id) =>
        await questionsCollection.DeleteOneAsync(x => x.Id == id);
}

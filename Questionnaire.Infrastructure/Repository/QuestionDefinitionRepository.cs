using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Questionnaire.Domain.Model;

namespace Questionnaire.Infrastructure.Repository;

public class QuestionDefinitionRepository : IQuestionDefinitionRepository
{
    private readonly IMongoCollection<QuestionDefinition> questionDefinitionCollection;

    public QuestionDefinitionRepository(IOptions<QuestionnaireDBSettings> questionnaireDBSettings)
    {
        var mongoClient = new MongoClient(questionnaireDBSettings.Value.ConnectionString);
        var mongoDB = mongoClient.GetDatabase(questionnaireDBSettings.Value.DatabaseName);

        questionDefinitionCollection = mongoDB.GetCollection<QuestionDefinition>(questionnaireDBSettings.Value.QuestionDefinitionsCollectionName);
    }

    public async Task<List<QuestionDefinition>> GetAllAsync() =>
        await questionDefinitionCollection.Find(_ => true).ToListAsync();

    public async Task<QuestionDefinition> GetByIdAsync(Guid id) =>
        await questionDefinitionCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(QuestionDefinition newQuestionDefinition) =>
        await questionDefinitionCollection.InsertOneAsync(newQuestionDefinition);

    public async Task UpdateAsync(Guid id, QuestionDefinition updatedQuestionDefinition) =>
        await questionDefinitionCollection.UpdateOneAsync(
            Builders<QuestionDefinition>.Filter.Eq(qd => qd.Id, id),
            Builders<QuestionDefinition>.Update
                .Set(qd => qd.Name, updatedQuestionDefinition.Name)
                .Set(qd => qd.Type, updatedQuestionDefinition.Type)
                .Set(qd => qd.UIType, updatedQuestionDefinition.UIType)
                //.Set(qd => qd.Validation, updatedQuestionDefinition.Validation)
            );

    public async Task DeleteAsync(Guid id) =>
        await questionDefinitionCollection.DeleteOneAsync(x => x.Id == id);
}

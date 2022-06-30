using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Questionnaire.Domain.Model;

namespace Questionnaire.Infrastructure.Repository
{
    public class QuestionDefinitionRepository : IQuestionDefinitionRepository
    {
        private readonly IMongoCollection<QuestionDefinition> questionDefinitionCollection;

        public QuestionDefinitionRepository(IOptions<QuestionnaireDBSettings> questionnaireDBSettings)
        {
            var mongoClient = new MongoClient(questionnaireDBSettings.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(questionnaireDBSettings.Value.DatabaseName);

            questionDefinitionCollection = mongoDB.GetCollection<QuestionDefinition>(questionnaireDBSettings.Value.QuestionDefinitionsCollectionName);
        }

        public async Task<List<QuestionDefinition>> Get() =>
            await questionDefinitionCollection.Find(_ => true).ToListAsync();

        public async Task<QuestionDefinition> Get(Guid id) =>
            await questionDefinitionCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task Create(QuestionDefinition newQuestionDefinition) =>
            await questionDefinitionCollection.InsertOneAsync(newQuestionDefinition);

        public async Task Update(Guid id, QuestionDefinition updatedQuestionDefinition) =>
            await questionDefinitionCollection.ReplaceOneAsync(x => x.Id == id, updatedQuestionDefinition);

        public async Task Delete(Guid id) =>
            await questionDefinitionCollection.DeleteOneAsync(x => x.Id == id);
    }
}

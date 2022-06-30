using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Questionnaire.Domain.Model;

namespace Questionnaire.Infrastructure.Repository
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly IMongoCollection<Survey> surveyCollection;

        public SurveyRepository(IOptions<QuestionnaireDBSettings> questionnaireDBSettings)
        {
            var mongoClient = new MongoClient(questionnaireDBSettings.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(questionnaireDBSettings.Value.DatabaseName);

            surveyCollection = mongoDB.GetCollection<Survey>(questionnaireDBSettings.Value.SurveysCollectionName);
        }

        public async Task<List<Survey>> Get() =>
            await surveyCollection.Find(_ => true).ToListAsync();

        public async Task<Survey> Get(Guid id) =>
            await surveyCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task Create(Survey newSurvey) =>
            await surveyCollection.InsertOneAsync(newSurvey);

        public async Task Update(Guid id, Survey updatedSurvey) =>
            await surveyCollection.ReplaceOneAsync(x => x.Id == id, updatedSurvey);

        public async Task Delete(Guid id) =>
            await surveyCollection.DeleteOneAsync(x => x.Id == id);
    }
}

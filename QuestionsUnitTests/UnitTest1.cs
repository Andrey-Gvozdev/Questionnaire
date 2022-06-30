using Mongo2Go;
using MongoDB.Driver;
using Moq;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;
using System.ComponentModel.DataAnnotations;

namespace QuestionsUnitTests
{
    public class Tests
    {
        MongoDbRunner runner;
        IMongoCollection<Question> testCollection;
        string databaseName = "Test";
        string testCollectionName = "TestCollection";
        IMongoDatabase database;
        IMongoClient client;
        Mock<IQuestionCrudService> questionCrudService = new Mock<IQuestionCrudService>();

        internal void CreateConnection()
        {
            runner = MongoDbRunner.Start();

            client = new MongoClient(runner.ConnectionString);
            database = client.GetDatabase(databaseName);
            testCollection = database.GetCollection<Question>(testCollectionName);
        }

        [SetUp]
        public void Setup()
        {
            CreateConnection();
        }

        [Test]
        public async Task QuestionCrudService_CreateAsync_InValid_Success()
        {
            var question = new Question
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Definition = new QuestionDefinition(),
                QuestionText = "testString",
                IsRequired = true
            };
            var questionVithSameId = new Question
            {
                Id = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                Definition = new QuestionDefinition(),
                QuestionText = "testString",
                IsRequired = true
            };

            await questionCrudService.Object.CreateAsync(question);
            try
            {
                await questionCrudService.Object.CreateAsync(questionVithSameId);
            }
            catch (ValidationException exception)
            {
                Assert.That(String.Concat("Item vith id: ",question.Id, " already exists"), Is.EqualTo(exception.Message));
            }
        }
    }
}
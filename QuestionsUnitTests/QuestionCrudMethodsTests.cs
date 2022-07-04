using AutoFixture;
using Mongo2Go;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Questionnaire.Domain.CustomExceptions;

namespace QuestionsUnitTests;

public class QuestionCrudMethodsTests
{
    MongoDbRunner runner;
    string databaseName = "Test";
    string testCollectionName = "TestCollection";
    IMongoCollection<Question> testCollection;
    IMongoDatabase database;
    IMongoClient client;
    Mock<IQuestionRepository> questionRepository = new Mock<IQuestionRepository>();
    QuestionCrudService questionCrudService;

    [SetUp]
    public void Setup()
    {
        CreateConnection();
        questionCrudService = new QuestionCrudService(questionRepository.Object);
    }

    [Test]
    public async Task QuestionCrudService_GetByIdAsync_Valid_Success()
    {
        Question question = CreateQuestion(new Guid("1fa85f64-5717-4562-b3fc-2c963f66afa6"));
        await testCollection.InsertOneAsync(question);

        SetupQuestionRepositoryGetByIdMethod(question.Id);
        Question expectedQuestion = await questionCrudService.GetByIdAsync(question.Id);

        expectedQuestion.Id.Should().Be(question.Id);

        DisposeRunner();
    }

    [Test]
    public async Task QuestionCrudService_GetByIdAsync_InValid_Success()
    {
        Question question = CreateQuestion(new Guid("1fa85f64-5717-4562-b3fc-2c963f66afa6"));

        SetupQuestionRepositoryGetByIdMethod(question.Id);
        Func<Task> getByIdAction = async () => await questionCrudService.GetByIdAsync(question.Id);

        await getByIdAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");

        DisposeRunner();
    }

    [Test]
    public async Task QuestionCrudService_CreateAsync_Valid_Success()
    {
        Question question = CreateQuestion(new Guid("5fa85f64-5717-4562-b3fc-2c963f66afa6"));
        Question questionVithDifferentId = CreateQuestion(new Guid("4fa85f64-5717-4562-b3fc-2c963f66afa6"));

        SetupQuestionRepositoryCreateMethod(question);
        await questionCrudService.CreateAsync(question);

        SetupQuestionRepositoryCreateMethod(questionVithDifferentId);
        await questionCrudService.CreateAsync(questionVithDifferentId);
        var amountQuestions = await testCollection.Find(_ => true).CountDocumentsAsync();

        amountQuestions.Should().Be(Convert.ToInt64(2));

        DisposeRunner();
    }

    [Test]
    public async Task QuestionCrudService_CreateAsync_InValid_Success()
    {
        Question question = CreateQuestion(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"));

        SetupQuestionRepositoryCreateMethod(question);
        await questionCrudService.CreateAsync(question);

        SetupQuestionRepositoryCreateMethod(question);
        SetupQuestionRepositoryGetByIdMethod(question.Id);
        Func<Task> createAction = async () => await questionCrudService.CreateAsync(question);

        await createAction.Should().ThrowAsync<ValidationException>();

        DisposeRunner();
    }

    private void CreateConnection()
    {
        runner = MongoDbRunner.Start();
        client = new MongoClient(runner.ConnectionString);
        database = client.GetDatabase(databaseName);
        testCollection = database.GetCollection<Question>(testCollectionName);
    }

    private void DisposeRunner()
    {
        runner.Dispose();
    }

    private Question CreateQuestion(Guid id)
    {
        Fixture fixture = new Fixture();
        Question question = fixture.Build<Question>()
            .With(q => q.Id, id)
            .Without(q => q.Definition)
            .Create();

        return question;
    }

    private void SetupQuestionRepositoryCreateMethod(Question question)
    {
        questionRepository.Setup(rep => rep.CreateAsync(question)).Returns(testCollection.InsertOneAsync(question));
    }

    private void SetupQuestionRepositoryGetByIdMethod(Guid id)
    {
        questionRepository.Setup(rep => rep.GetByIdAsync(id)).Returns(testCollection.Find(x => x.Id == id).FirstOrDefaultAsync());
    }
}
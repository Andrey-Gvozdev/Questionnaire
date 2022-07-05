using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using Questionnaire.Domain.Model;
using Questionnaire.Domain.Services.CRUDServices;
using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using Questionnaire.Domain.CustomExceptions;
using Mongo2Go;

namespace QuestionsUnitTests;

public class QuestionCrudMethodsTests
{
    private const string databaseName = "Test";
    private const string testCollectionName = "TestCollection";

    private MongoDbRunner runner;
    private IMongoCollection<Question> testCollection;
    private IMongoDatabase database;
    private IMongoClient client;
    private readonly Mock<IQuestionRepository> questionRepository = new Mock<IQuestionRepository>();
    private QuestionCrudService questionCrudService;
    private QuestionFactory questionFactory;

    [SetUp]
    public void Setup()
    {
        runner = MongoDbRunner.Start();
        client = new MongoClient(runner.ConnectionString);
        database = client.GetDatabase(databaseName);
        testCollection = database.GetCollection<Question>(testCollectionName);
        questionCrudService = new QuestionCrudService(questionRepository.Object);
        questionFactory = new QuestionFactory(testCollection, questionRepository);
    }

    [TearDown]
    public void DisposeRunner()
    {
        runner.Dispose();
    }

    [Test]
    public async Task GetByIdAsync_ShouldGet()
    {
        var question = questionFactory.CreateQuestion(Guid.NewGuid());
        await testCollection.InsertOneAsync(question);

        questionFactory.SetupQuestionRepositoryGetByIdMethod(question.Id);
        var expectedQuestion = await questionCrudService.GetByIdAsync(question.Id);

        expectedQuestion.Should().NotBeNull();
        expectedQuestion.Id.Should().Be(question.Id);

        DisposeRunner();
    }

    [Test]
    public async Task GetByIdAsync_ShouldThrowIfQuestionNotExist()
    {
        var question = questionFactory.CreateQuestion(Guid.NewGuid());

        questionFactory.SetupQuestionRepositoryGetByIdMethod(question.Id);
        var getByIdAction = async () => await questionCrudService.GetByIdAsync(question.Id);

        await getByIdAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");

        DisposeRunner();
    }

    [Test]
    public async Task CreateAsync_ShouldCreate()
    {
        var question = questionFactory.CreateQuestion(Guid.NewGuid());
        var questionVithDifferentId = questionFactory.CreateQuestion(Guid.NewGuid());

        questionFactory.SetupQuestionRepositoryCreateMethod(question);
        await questionCrudService.CreateAsync(question);

        questionFactory.SetupQuestionRepositoryCreateMethod(questionVithDifferentId);
        await questionCrudService.CreateAsync(questionVithDifferentId);
        var amountQuestions = await testCollection.Find(_ => true).CountDocumentsAsync();

        amountQuestions.Should().Be(Convert.ToInt64(2));

        DisposeRunner();
    }

    [Test]
    public async Task CreateAsync_ShouldThrowIfValidationFailed()
    {
        var question = questionFactory.CreateQuestion(Guid.NewGuid());

        questionFactory.SetupQuestionRepositoryCreateMethod(question);
        await questionCrudService.CreateAsync(question);

        questionFactory.SetupQuestionRepositoryCreateMethod(question);
        questionFactory.SetupQuestionRepositoryGetByIdMethod(question.Id);
        var createAction = async () => await questionCrudService.CreateAsync(question);

        await createAction.Should().ThrowAsync<ValidationException>();

        DisposeRunner();
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdate()
    {
        var changedText = "changedText";
        var id = Guid.NewGuid();
        var question = questionFactory.CreateQuestion(id);
        var updatedQuestion = questionFactory.CreateQuestion(id, changedText);

        await testCollection.InsertOneAsync(question);
        questionFactory.SetupQuestionRepositoryUpdateMethod(id, updatedQuestion);
        questionFactory.SetupQuestionRepositoryGetByIdMethod(id);
        await questionCrudService.UpdateAsync(id, updatedQuestion);

        questionFactory.SetupQuestionRepositoryGetByIdMethod(id);
        var expectedQuestion = await questionCrudService.GetByIdAsync(id);

        expectedQuestion.QuestionText.Should().Be(changedText);

        DisposeRunner();
    }

    [Test]
    public async Task UpdateAsync_ShouldThrowIfValidationFailed()
    {
        var changedText = "changedText";
        var id = Guid.NewGuid();
        var updatedQuestion = questionFactory.CreateQuestion(id, changedText);

        questionFactory.SetupQuestionRepositoryUpdateMethod(id, updatedQuestion);
        questionFactory.SetupQuestionRepositoryGetByIdMethod(id);

        var updateAction = async () => await questionCrudService.UpdateAsync(id, updatedQuestion);

        await updateAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");

        DisposeRunner();
    }

    [Test]
    public async Task DeleteAsync_ShouldDele()
    {
        var id = Guid.NewGuid();
        var question = questionFactory.CreateQuestion(id);
        
        await testCollection.InsertOneAsync(question);

        questionFactory.SetupQuestionRepositoryGetByIdMethod(id);
        questionFactory.SetupQuestionRepositoryDeleteMethod(id);

        var deleteAction = async () => await questionCrudService.DeleteAsync(id);
        var getByIdAction = async () => await questionCrudService.GetByIdAsync(id);

        await deleteAction.Should().NotThrowAsync<NotFoundException>();
        questionFactory.SetupQuestionRepositoryGetByIdMethod(id);
        await getByIdAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");
        
        DisposeRunner();
    }

    [Test]
    public async Task DeleteAsync_ShouldThrowIfValidationFailed()
    {
        var id = Guid.NewGuid();
        var question = questionFactory.CreateQuestion(id);

        questionFactory.SetupQuestionRepositoryGetByIdMethod(id);
        questionFactory.SetupQuestionRepositoryDeleteMethod(id);

        var deleteAction = async () => await questionCrudService.DeleteAsync(id);

        await deleteAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");

        DisposeRunner();
    }
}
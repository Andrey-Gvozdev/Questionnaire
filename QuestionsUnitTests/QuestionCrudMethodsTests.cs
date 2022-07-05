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
    private const string databaseName = "Test";
    private const string testCollectionName = "TestCollection";
    
    private MongoDbRunner runner;
    private IMongoCollection<Question> testCollection;
    private IMongoDatabase database;
    private IMongoClient client;
    private readonly Mock<IQuestionRepository> questionRepository = new Mock<IQuestionRepository>();
    private QuestionCrudService questionCrudService;
    private Fixture fixture = new Fixture();

    [SetUp]
    public void Setup()
    {
        CreateConnection();
        questionCrudService = new QuestionCrudService(questionRepository.Object);
    }

    [Test]
    public async Task GetByIdAsync_ShouldGet()
    {
        var question = CreateQuestion(Guid.NewGuid());
        await testCollection.InsertOneAsync(question);

        SetupQuestionRepositoryGetByIdMethod(question.Id);
        var expectedQuestion = await questionCrudService.GetByIdAsync(question.Id);

        expectedQuestion.Should().NotBeNull();
        expectedQuestion.Id.Should().Be(question.Id);

        DisposeRunner();
    }

    [Test]
    public async Task GetByIdAsync_ShouldThrowIfQuestionNotExist()
    {
        var question = CreateQuestion(Guid.NewGuid());

        SetupQuestionRepositoryGetByIdMethod(question.Id);
        var getByIdAction = async () => await questionCrudService.GetByIdAsync(question.Id);

        await getByIdAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");

        DisposeRunner();
    }

    [Test]
    public async Task CreateAsync_ShouldCreate()
    {
        var question = CreateQuestion(Guid.NewGuid());
        var questionVithDifferentId = CreateQuestion(Guid.NewGuid());

        SetupQuestionRepositoryCreateMethod(question);
        await questionCrudService.CreateAsync(question);

        SetupQuestionRepositoryCreateMethod(questionVithDifferentId);
        await questionCrudService.CreateAsync(questionVithDifferentId);
        var amountQuestions = await testCollection.Find(_ => true).CountDocumentsAsync();

        amountQuestions.Should().Be(Convert.ToInt64(2));

        DisposeRunner();
    }

    [Test]
    public async Task CreateAsync_ShouldThrowIfValidationFailed()
    {
        var question = CreateQuestion(Guid.NewGuid());

        SetupQuestionRepositoryCreateMethod(question);
        await questionCrudService.CreateAsync(question);

        SetupQuestionRepositoryCreateMethod(question);
        SetupQuestionRepositoryGetByIdMethod(question.Id);
        var createAction = async () => await questionCrudService.CreateAsync(question);

        await createAction.Should().ThrowAsync<ValidationException>();

        DisposeRunner();
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdate()
    {
        var changedText = "changedText";
        var id = Guid.NewGuid();
        var question = CreateQuestion(id);
        var updatedQuestion = CreateQuestion(id, changedText);

        await testCollection.InsertOneAsync(question);
        SetupQuestionRepositoryUpdateMethod(id, updatedQuestion);
        SetupQuestionRepositoryGetByIdMethod(id);
        await questionCrudService.UpdateAsync(id, updatedQuestion);

        SetupQuestionRepositoryGetByIdMethod(id);
        var expectedQuestion = await questionCrudService.GetByIdAsync(id);

        expectedQuestion.QuestionText.Should().Be(changedText);

        DisposeRunner();
    }

    [Test]
    public async Task UpdateAsync_ShouldThrowIfValidationFailed()
    {
        var changedText = "changedText";
        var id = Guid.NewGuid();
        var updatedQuestion = CreateQuestion(id, changedText);

        SetupQuestionRepositoryUpdateMethod(id, updatedQuestion);
        SetupQuestionRepositoryGetByIdMethod(id);

        var updateAction = async () => await questionCrudService.UpdateAsync(id, updatedQuestion);

        await updateAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");

        DisposeRunner();
    }

    [Test]
    public async Task DeleteAsync_ShouldDele()
    {
        var id = Guid.NewGuid();
        var question = CreateQuestion(id);
        
        await testCollection.InsertOneAsync(question);

        SetupQuestionRepositoryGetByIdMethod(id);
        SetupQuestionRepositoryDeleteMethod(id);

        var deleteAction = async () => await questionCrudService.DeleteAsync(id);
        var getByIdAction = async () => await questionCrudService.GetByIdAsync(id);

        await deleteAction.Should().NotThrowAsync<NotFoundException>();
        SetupQuestionRepositoryGetByIdMethod(id);
        await getByIdAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");
        
        DisposeRunner();
    }

    [Test]
    public async Task DeleteAsync_ShouldThrowIfValidationFailed()
    {
        var id = Guid.NewGuid();
        var question = CreateQuestion(id);

        SetupQuestionRepositoryGetByIdMethod(id);
        SetupQuestionRepositoryDeleteMethod(id);

        var deleteAction = async () => await questionCrudService.DeleteAsync(id);

        await deleteAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");

        DisposeRunner();
    }

    private void CreateConnection()
    {
        runner = MongoDbRunner.Start();
        client = new MongoClient(runner.ConnectionString);
        database = client.GetDatabase(databaseName);
        testCollection = database.GetCollection<Question>(testCollectionName);
    }

    [TearDown]
    public void DisposeRunner()
    {
        runner.Dispose();
    }

    private Question CreateQuestion(Guid id, string questionText = "defaultText")
    {
        var question = fixture.Build<Question>()
            .With(q => q.Id, id)
            .With(q => q.QuestionText, questionText)
            .Without(q => q.Definition)
            .Create();

        return question;
    }

    private void SetupQuestionRepositoryCreateMethod(Question question)
    {
        questionRepository
            .Setup(rep => rep.CreateAsync(question))
            .Returns(testCollection.InsertOneAsync(question));
    }

    private void SetupQuestionRepositoryGetByIdMethod(Guid id)
    {
        questionRepository
            .Setup(rep => rep.GetByIdAsync(id))
            .Returns(testCollection.Find(x => x.Id == id).FirstOrDefaultAsync());
    }

    private void SetupQuestionRepositoryUpdateMethod(Guid id, Question updatedQuestion)
    {
        questionRepository
            .Setup(rep => rep.UpdateAsync(id, updatedQuestion))
            .Returns(testCollection.UpdateOneAsync(
                Builders<Question>.Filter.Eq(q => q.Id, id),
                Builders<Question>.Update
                    .Set(q => q.Definition, updatedQuestion.Definition)
                    .Set(q => q.QuestionText, updatedQuestion.QuestionText)
                    .Set(q => q.IsRequired, updatedQuestion.IsRequired)
            ));
    }

    private void SetupQuestionRepositoryDeleteMethod(Guid id)
    {
        questionRepository
            .Setup(rep => rep.DeleteAsync(id))
            .Returns(testCollection.DeleteOneAsync(x => x.Id == id));
    }
}
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
    // TODO: use access modifiers
    MongoDbRunner runner;
    // TODO: these string can be constants
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
    // GetByIdAsync_ShouldGet
    public async Task QuestionCrudService_GetByIdAsync_Valid_Success()
    {
        // TODO: use var
        // TODO: use Guid.NewGuid()
        Question question = CreateQuestion(new Guid("1fa85f64-5717-4562-b3fc-2c963f66afa6"));
        await testCollection.InsertOneAsync(question);

        SetupQuestionRepositoryGetByIdMethod(question.Id);
        Question expectedQuestion = await questionCrudService.GetByIdAsync(question.Id);

        // TODO: check if expectedQuestion is not null
        expectedQuestion.Id.Should().Be(question.Id);

        DisposeRunner();
    }

    [Test]
    // GetByIdAsync_ShouldThrowIfQuestionNotExist
    public async Task QuestionCrudService_GetByIdAsync_InValid_Success()
    {
        Question question = CreateQuestion(new Guid("1fa85f64-5717-4562-b3fc-2c963f66afa6"));

        SetupQuestionRepositoryGetByIdMethod(question.Id);
        Func<Task> getByIdAction = async () => await questionCrudService.GetByIdAsync(question.Id);

        await getByIdAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");

        DisposeRunner();
    }

    [Test]
    // CreateAsync_ShouldCreate
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
    // CreateAsync_ShouldThrowIfQuestionInvalid or CreateAsync_ShouldThrowIfValidationFailed
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

    [Test]
    public async Task QuestionCrudService_UpdateAsync_Valid_Success()
    {
        Guid id = new Guid("9fa85f64-5717-4562-b3fc-2c963f66afa6");
        string changedText = "changedText";
        Question question = CreateQuestion(id);
        Question updatedQuestion = CreateQuestion(id, changedText);
        await testCollection.InsertOneAsync(question);

        SetupQuestionRepositoryUpdateMethod(id, updatedQuestion);
        SetupQuestionRepositoryGetByIdMethod(id);
        await questionCrudService.UpdateAsync(id, updatedQuestion);

        SetupQuestionRepositoryGetByIdMethod(id);
        Question expectedQuestion = await questionCrudService.GetByIdAsync(id);

        expectedQuestion.QuestionText.Should().Be(changedText);

        DisposeRunner();
    }

    [Test]
    public async Task QuestionCrudService_UpdateAsync_InValid_Success()
    {
        Guid id = new Guid("9fa85f64-5717-4562-b3fc-2c963f66afa6");
        string changedText = "changedText";
        Question updatedQuestion = CreateQuestion(id, changedText);

        SetupQuestionRepositoryUpdateMethod(id, updatedQuestion);
        SetupQuestionRepositoryGetByIdMethod(id);

        Func<Task> updateAction = async () => await questionCrudService.UpdateAsync(id, updatedQuestion);

        await updateAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");

        DisposeRunner();
    }

    [Test]
    public async Task QuestionCrudService_DeleteAsync_Valid_Success()
    {
        Guid id = new Guid("9fa85f64-5717-4562-b3fc-2c963f66afa6");
        Question question = CreateQuestion(id);
        await testCollection.InsertOneAsync(question);

        SetupQuestionRepositoryGetByIdMethod(id);
        SetupQuestionRepositoryDeleteMethod(id);

        Func<Task> deleteAction = async () => await questionCrudService.DeleteAsync(id);
        Func<Task> getByIdAction = async () => await questionCrudService.GetByIdAsync(id);

        await deleteAction.Should().NotThrowAsync<NotFoundException>();
        SetupQuestionRepositoryGetByIdMethod(id);
        await getByIdAction.Should().ThrowAsync<NotFoundException>().WithMessage("Item not found");
        
        DisposeRunner();
    }

    [Test]
    public async Task QuestionCrudService_DeleteAsync_InValid_Success()
    {
        Guid id = new Guid("9fa85f64-5717-4562-b3fc-2c963f66afa6");
        Question question = CreateQuestion(id);

        SetupQuestionRepositoryGetByIdMethod(id);
        SetupQuestionRepositoryDeleteMethod(id);

        Func<Task> deleteAction = async () => await questionCrudService.DeleteAsync(id);

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

    // TODO: use [TearDown] attribute to cleanup
    // your test "environment" after test execution
    private void DisposeRunner()
    {
        runner.Dispose();
    }

    private Question CreateQuestion(Guid id, string questionText = "defaultText")
    {
        // TODO: make Fixture private field
        Fixture fixture = new Fixture();
        Question question = fixture.Build<Question>()
            .With(q => q.Id, id)
            .With(q => q.QuestionText, questionText)
            .Without(q => q.Definition)
            .Create();

        return question;
    }

    private void SetupQuestionRepositoryCreateMethod(Question question)
    {
        // TODO: split code into multiple lines
        questionRepository
            .Setup(rep => rep.CreateAsync(question))
            .Returns(testCollection.InsertOneAsync(question));
    }

    private void SetupQuestionRepositoryGetByIdMethod(Guid id)
    {
        questionRepository.Setup(rep => rep.GetByIdAsync(id)).Returns(testCollection.Find(x => x.Id == id).FirstOrDefaultAsync());
    }

    private void SetupQuestionRepositoryUpdateMethod(Guid id, Question updatedQuestion)
    {
        questionRepository.Setup(rep => rep.UpdateAsync(id, updatedQuestion)).Returns(testCollection.UpdateOneAsync(
            Builders<Question>.Filter.Eq(q => q.Id, id),
            Builders<Question>.Update
                .Set(q => q.Definition, updatedQuestion.Definition)
                .Set(q => q.QuestionText, updatedQuestion.QuestionText)
                .Set(q => q.IsRequired, updatedQuestion.IsRequired)
            ));
    }

    private void SetupQuestionRepositoryDeleteMethod(Guid id)
    {
        questionRepository.Setup(rep => rep.DeleteAsync(id)).Returns(testCollection.DeleteOneAsync(x => x.Id == id));
    }
}
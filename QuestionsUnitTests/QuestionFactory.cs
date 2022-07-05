using AutoFixture;
using MongoDB.Driver;
using Moq;
using Questionnaire.Domain.Model;

namespace QuestionsUnitTests;

public class QuestionFactory
{
    private IMongoCollection<Question> testCollection;
    private readonly Mock<IQuestionRepository> questionRepository;
    private Fixture fixture = new Fixture();

    public QuestionFactory(IMongoCollection<Question> testCollection, Mock<IQuestionRepository> questionRepository)
    {
        this.testCollection = testCollection;
        this.questionRepository = questionRepository;
    }

    public Question CreateQuestion(Guid id, string questionText = "defaultText")
    {
        var question = fixture.Build<Question>()
            .With(q => q.Id, id)
            .With(q => q.QuestionText, questionText)
            .Without(q => q.Definition)
            .Create();

        return question;
    }

    public void SetupQuestionRepositoryCreateMethod(Question question)
    {
        questionRepository
            .Setup(rep => rep.CreateAsync(question))
            .Returns(testCollection.InsertOneAsync(question));
    }

    public void SetupQuestionRepositoryGetByIdMethod(Guid id)
    {
        questionRepository
            .Setup(rep => rep.GetByIdAsync(id))
            .Returns(testCollection.Find(x => x.Id == id).FirstOrDefaultAsync());
    }

    public void SetupQuestionRepositoryUpdateMethod(Guid id, Question updatedQuestion)
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

    public void SetupQuestionRepositoryDeleteMethod(Guid id)
    {
        questionRepository
            .Setup(rep => rep.DeleteAsync(id))
            .Returns(testCollection.DeleteOneAsync(x => x.Id == id));
    }
}
